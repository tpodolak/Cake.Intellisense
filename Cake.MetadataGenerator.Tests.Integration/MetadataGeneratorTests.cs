﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;
namespace Cake.MetadataGenerator.Tests.Integration
{
    public class MetadataGeneratorTests
    {
        private const string DefaultFramework = ".NETFramework,Version=v4.5";
        private readonly Application installisenseGenerator = new Application();

        [Theory]
        [InlineData("Cake.Common", DefaultFramework)]
        public void GenerateCanGenerateMetadataForPackageTest(string package, string framework)
        {
            var options = CreateMetadataGeneratorOptions(package, framework);
            var result = installisenseGenerator.Run(options);
            VerifyGeneratorResult(result, VerifyAliasAssemblyContent);
        }

        [Theory]
        [InlineData(DefaultFramework)]
        public void GenerateCanGenerateMetadataForCakeCoreLibTest(string framework)
        {
            var options = CreateMetadataGeneratorOptions("Cake.Core", framework);
            var result = installisenseGenerator.Run(options);

            VerifyGeneratorResult(result, VerifyScriptEngineAssemblyContent);
        }

        private void VerifyGeneratorResult(GeneratorResult result, Action<Assembly, Assembly> assemblyVerifier)
        {
            result.Should().NotBeNull();
            result.EmitedAssemblies.Count.Should().BeGreaterThan(0);
            result.SourceAssemblies.Count.Should().BeGreaterThan(0);
            result.SourceAssemblies.Should().HaveSameCount(result.EmitedAssemblies);

            for (var i = 0; i < result.SourceAssemblies.Count; i++)
            {
                VerifyGeneratorResult(result.SourceAssemblies[i], result.EmitedAssemblies[i], assemblyVerifier);
            }
        }

        private static void VerifyGeneratorResult(Assembly sourcAssembly, Assembly emitedAssembly, Action<Assembly, Assembly> assemblyVerifier)
        {
            sourcAssembly.Should().NotBeNull();
            emitedAssembly.Should().NotBeNull();

            var referencedAssemblies = emitedAssembly.GetReferencedAssemblies().Select(val => val.FullName);
            var locations =
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(val => referencedAssemblies.Contains(val.FullName))
                    .Select(val => val.Location)
                    .ToList();
            locations.ForEach(val => File.Copy(val, Path.Combine(Environment.CurrentDirectory, Path.GetFileName(val)), true));

            assemblyVerifier(sourcAssembly, emitedAssembly);
        }

        private void VerifyAliasAssemblyContent(Assembly sourceAssembly, Assembly emitedAssembly)
        {
            VerifyAssemblyContent(sourceAssembly, emitedAssembly, GetCakeAliasTypes, GetCakeAliasMethods, SameAliasMethod);
        }

        private void VerifyScriptEngineAssemblyContent(Assembly sourceAssembly, Assembly emitedAssembly)
        {
            VerifyAssemblyContent(sourceAssembly, emitedAssembly, GetCakeScriptHostTypes, GetCakeScriptEngineMethods, SameEngineMethod);
        }

        private void VerifyAssemblyContent(Assembly sourceAssembly, Assembly emitedAssembly, Func<Assembly, List<Type>> typeExtractor, Func<Type, List<MethodInfo>> sourceMethodExtract, Func<MethodInfo, MethodInfo, bool> methodMatcher)
        {
            var wrongGeneratedMethods = new List<MethodInfo>();
            var missingMethods = new List<MethodInfo>();
            var wrongGeneratedProperties = new List<MethodInfo>();
            var missingProperies = new List<MethodInfo>();

            var sourceTypes = typeExtractor(sourceAssembly).ToList();
            var emitedTypes = emitedAssembly.GetExportedTypes().ToDictionary(key => key.FullName);

            emitedTypes.Values.Should().HaveSameCount(sourceTypes);
            emitedTypes.Values.Should().OnlyContain(type => type.IsPublic);

            foreach (var sourceType in sourceTypes)
            {
                var generatedType = emitedTypes[sourceType.FullName + "Metadata"];
                var sourceMethods = sourceMethodExtract(sourceType);
                var emitedMethods = generatedType.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(val => !val.IsSpecialName).ToList();
                var emitedProperties = generatedType.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(val => val.IsSpecialName).ToList();
                var sourceProperties =
                    sourceType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .Where(
                            val =>
                                val.GetCustomAttributes().Any(x => x.GetType().Name == CakeAttributeNames.CakePropertyAlias))
                        .ToList();

                if (!sourceMethods.Any() && emitedMethods.Any())
                    wrongGeneratedMethods.AddRange(emitedMethods);

                missingMethods.AddRange(sourceMethods.Where(sourceMethod => !emitedMethods.Any(val => methodMatcher(sourceMethod, val))));
                wrongGeneratedMethods.AddRange(emitedMethods.Where(emitedMethod => !sourceMethods.Any(val => methodMatcher(val, emitedMethod))));

                if (!sourceProperties.Any() && emitedProperties.Any())
                    wrongGeneratedProperties.AddRange(emitedMethods);

                missingProperies.AddRange(sourceProperties.Where(sourceMethod => emitedProperties.All(val => $"get_{sourceMethod.Name}" != val.Name)));
                wrongGeneratedProperties.AddRange(emitedProperties.Where(emitedMethod => sourceProperties.All(val => emitedMethod.Name != $"get_{val.Name}")));
            }

            wrongGeneratedMethods.Should().BeEmpty();
            missingMethods.Should().BeEmpty();
            missingProperies.Should().BeEmpty();
            wrongGeneratedProperties.Should().BeEmpty();
        }

        private string[] CreateMetadataGeneratorOptions(string package, string targetFramework, string version = null)
        {
            return new[]
            {
                "--Package", package, "--PackageVersion", version ?? string.Empty, "--TargetFramework", targetFramework,
                "--OutputFolder", Path.Combine(Environment.CurrentDirectory, "Result")
            };
        }

        private bool SameAliasMethod(MethodInfo sourceMethod, MethodInfo emitedMethod)
        {
            var sourceMethodName = sourceMethod.ToString();
            var emitedMethodName = emitedMethod.ToString();
            if (sourceMethod.GetParameters().Length > 0)
            {
                var firstParam = sourceMethod.GetParameters().First().ParameterType.FullName;
                sourceMethodName = sourceMethodName.Replace($"({firstParam}, ", "(")
                                                   .Replace($"({firstParam}", "(");
            }

            return sourceMethodName.Equals(emitedMethodName, StringComparison.InvariantCulture);
        }

        private bool SameEngineMethod(MethodInfo sourceMethod, MethodInfo emitedMethod)
        {
            return sourceMethod.ToString() == emitedMethod.ToString();
        }

        private static List<Type> GetCakeAliasTypes(Assembly assembly)
        {
            return assembly.GetExportedTypes()
                .Where(val => val.GetCustomAttributes().Any(attr => attr.GetType().FullName == "Cake.Core.Annotations.CakeAliasCategoryAttribute"))
                .ToList();
        }

        private static List<Type> GetCakeScriptHostTypes(Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(val => val.FullName == "Cake.Core.Scripting.ScriptHost").ToList();
        }

        private static List<MethodInfo> GetCakeAliasMethods(Type sourceType)
        {
            return sourceType.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(method => method.GetCustomAttributes().Any(attr => attr.GetType().FullName == "Cake.Core.Annotations.CakeMethodAliasAttribute"))
                .ToList();
        }

        private static List<MethodInfo> GetCakeScriptEngineMethods(Type type)
        {
            return type.FullName == "Cake.Core.Scripting.ScriptHost"
                ? type.GetMethods().Where(val => !val.IsSpecialName && val.DeclaringType != typeof(object)).ToList()
                : new List<MethodInfo>();
        }
    }
}
