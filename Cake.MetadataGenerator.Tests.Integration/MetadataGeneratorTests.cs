using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Cake.MetadataGenerator.CommandLine;
using FluentAssertions;
using Xunit;
namespace Cake.MetadataGenerator.Tests.Integration
{
    public class MetadataGeneratorTests
    {
        private const string defaultFramework = ".NETFramework,Version=v4.5";
        private readonly MetadataGenerator generator;

        public MetadataGeneratorTests()
        {
            generator = new MetadataGenerator();
        }

        [Theory]
        [InlineData("Cake.Common", defaultFramework)]
        public void GenerateCanGenerateMetadataForPackageTest(string package, string framework)
        {
            var options = CreateMetadataGeneratorOptions(package, framework);
            var result = generator.Generate(options);
            VerifyGeneratorResult(result, VerifyAliasAssemblyContent);
        }

        [Theory]
        [InlineData(defaultFramework)]
        public void GenerateCanGenerateMetadataForCakeCoreLibTest(string framework)
        {
            var options = CreateMetadataGeneratorOptions("Cake.Core", framework);
            var result = generator.Generate(options);
            VerifyGeneratorResult(result, VerifyScriptEngineAssemblyContent);
        }

        private void VerifyGeneratorResult(GeneratorResult result, Action<Assembly[], Assembly> assemblyVerifier)
        {
            result.Should().NotBeNull();
            result.SourceAssemblies.Should().HaveCount(count => count > 0);
            result.EmitedAssembly.Should().NotBeNull();
            VerifyAliasAssemblyContent(result.SourceAssemblies, result.EmitedAssembly);
        }

        private void VerifyAliasAssemblyContent(IEnumerable<Assembly> sourceAssemblies, Assembly emitedAssembly)
        {
            VerifyAssemblyContent(sourceAssemblies, emitedAssembly, GetCakeAliasTypes, GetCakeAliasMethods, SameAliasMethod);
        }

        private void VerifyScriptEngineAssemblyContent(IEnumerable<Assembly> sourceAssemblies, Assembly emitedAssembly)
        {
            VerifyAssemblyContent(sourceAssemblies, emitedAssembly, GetCakeScriptHostTypes, GetCakeScriptEngineMethods, SameEngineMethod);
        }

        private void VerifyAssemblyContent(IEnumerable<Assembly> sourceAssemblies, Assembly emitedAssembly, Func<Assembly, List<Type>> typeExtractor, Func<Type, List<MethodInfo>> sourceMethodExtract, Func<MethodInfo, MethodInfo, bool> methodMatcher)
        {
            var sourceTypes = sourceAssemblies.SelectMany(typeExtractor).ToList();

            var emitedTypes = emitedAssembly.GetExportedTypes().ToDictionary(key => key.FullName);

            var generatedTypes = sourceTypes.Where(val => emitedTypes.ContainsKey(val.FullName + "Metadata")).Select(val => emitedTypes[val.FullName + "Metadata"]).ToList();

            generatedTypes.Should().HaveSameCount(sourceTypes);
            generatedTypes.All(type => type.IsPublic).Should().BeTrue();
            var wrongGeneratedMethods = new Dictionary<MethodInfo, List<MethodInfo>>();
            foreach (var sourceType in sourceTypes)
            {
                var generatedType = emitedTypes[sourceType.FullName + "Metadata"];
                var sourceMethods = sourceMethodExtract(sourceType);
                var emitedMethods = generatedType.GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();

                foreach (var methodInfo in sourceMethods.Where(sourceMethod => !emitedMethods.Any(val => methodMatcher(sourceMethod, val))))
                {
                    wrongGeneratedMethods.Add(methodInfo, emitedMethods);
                }
            }

            wrongGeneratedMethods.Should().BeEmpty();
        }

        private MetadataGeneratorOptions CreateMetadataGeneratorOptions(string package, string packageTargetFramework, string version = null)
        {
            return new MetadataGeneratorOptions
            {
                OutputFolder = Path.Combine(Environment.CurrentDirectory, "Result"),
                Package = package,
                PackageVersion = version,
                PackageFrameworkTargetVersion = packageTargetFramework
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
                ? type.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(val => !val.IsSpecialName).ToList()
                : new List<MethodInfo>();
        }
    }
}
