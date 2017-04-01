using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Intellisense.CodeGeneration.MetadataGenerators;
using Cake.Intellisense.Tests.Integration.Extensions;
using FluentAssertions;
using FluentAssertions.Primitives;
using static Cake.Intellisense.Constants.MetadataGeneration;
using TypeExtensions = Cake.Intellisense.Tests.Integration.Extensions.TypeExtensions;

namespace Cake.Intellisense.Tests.Integration.Assertions
{
    public class GeneratorResultAssertions : ReferenceTypeAssertions<GeneratorResult, GeneratorResultAssertions>
    {
        protected override string Context { get; } = nameof(GeneratorResult);

        public GeneratorResultAssertions(GeneratorResult subject)
        {
            Subject = subject;
        }

        public AndConstraint<GeneratorResultAssertions> GenerateValidCakeAssemblies()
        {
            VerifyGeneratorResult(Subject, VerifyAliasAssemblyContent);
            return new AndConstraint<GeneratorResultAssertions>(this);
        }

        public AndConstraint<GeneratorResultAssertions> GenerateValidCakeCoreAssemblies()
        {
            VerifyGeneratorResult(Subject, VerifyScriptEngineAssemblyContent);
            return new AndConstraint<GeneratorResultAssertions>(this);
        }

        private void VerifyGeneratorResult(GeneratorResult result, Action<Assembly, Assembly> assemblyVerifier)
        {
            result.EmitedAssemblies.Count.Should().BeGreaterThan(0);
            result.SourceAssemblies.Count.Should().BeGreaterThan(0);
            result.SourceAssemblies.Should().HaveSameCount(result.EmitedAssemblies);

            for (var i = 0; i < result.SourceAssemblies.Count; i++)
            {
                VerifyGeneratorResult(result.SourceAssemblies[i], result.EmitedAssemblies[i], assemblyVerifier);
            }
        }

        private void VerifyGeneratorResult(Assembly sourcAssembly, Assembly emitedAssembly, Action<Assembly, Assembly> assemblyVerifier)
        {
            sourcAssembly.Should().NotBeNull();
            emitedAssembly.Should().NotBeNull();
            emitedAssembly.GetName().Version.ToString().Should().Be(sourcAssembly.GetName().Version.ToString());
            emitedAssembly.GetName().Name.Should().Be($"{sourcAssembly.GetName().Name}.{MetadataClassSuffix}");
            assemblyVerifier(sourcAssembly, emitedAssembly);
        }

        private void VerifyAliasAssemblyContent(Assembly sourceAssembly, Assembly emitedAssembly)
        {
            VerifyAssemblyContent(sourceAssembly, emitedAssembly, AssemblyExtensions.GetCakeAliasTypes, TypeExtensions.GetCakeAliasMethods, MethodInfoExtensions.IsSameCakeAliasMethod);
        }

        private void VerifyScriptEngineAssemblyContent(Assembly sourceAssembly, Assembly emitedAssembly)
        {
            VerifyAssemblyContent(sourceAssembly, emitedAssembly, AssemblyExtensions.GetCakeScriptHostTypes, TypeExtensions.GetCakeScriptEngineMethods, MethodInfoExtensions.IsSameCakeEngineMethod);
        }

        private void VerifyAssemblyContent(
            Assembly sourceAssembly,
            Assembly emitedAssembly,
            Func<Assembly, IEnumerable<Type>> typeExtractor,
            Func<Type, IEnumerable<MethodInfo>> sourceMethodExtract,
            Func<MethodInfo, MethodInfo, bool> methodMatcher)
        {
            var wrongGeneratedMethods = new List<MethodInfo>();
            var missingMethods = new List<MethodInfo>();
            var wrongGeneratedProperties = new List<MethodInfo>();
            var missingProperties = new List<MethodInfo>();

            var sourceTypes = typeExtractor(sourceAssembly).ToList();
            var emitedTypes = emitedAssembly.GetExportedTypes().ToDictionary(key => key.FullName);

            emitedTypes.Values.Should().HaveSameCount(sourceTypes);
            emitedTypes.Values.Where(type => type.IsPublic).Should().HaveSameCount(emitedTypes.Values);

            foreach (var sourceType in sourceTypes)
            {
                var generatedType = emitedTypes[sourceType.FullName + MetadataClassSuffix];
                var sourceMethods = sourceMethodExtract(sourceType).ToList();
                var emitedMethods = generatedType.GetCakeMetadataMethods().ToList();
                var emitedProperties = generatedType.GetCakeMetadataPropertes().ToList();
                var sourceProperties = sourceType.GetCakeProperties().ToList();

                if (!sourceMethods.Any() && emitedMethods.Any())
                    wrongGeneratedMethods.AddRange(emitedMethods);

                missingMethods.AddRange(sourceMethods.Where(
                    sourceMethod => !emitedMethods.Any(val => methodMatcher(sourceMethod, val))));

                wrongGeneratedMethods.AddRange(
                    emitedMethods.Where(emitedMethod => !sourceMethods.Any(val => methodMatcher(val, emitedMethod))));

                if (!sourceProperties.Any() && emitedProperties.Any())
                    wrongGeneratedProperties.AddRange(emitedMethods);

                missingProperties.AddRange(
                    sourceProperties.Where(
                        sourceMethod => emitedProperties.All(val => $"get_{sourceMethod.Name}" != val.Name)));
                wrongGeneratedProperties.AddRange(
                    emitedProperties.Where(
                        emitedMethod => sourceProperties.All(val => emitedMethod.Name != $"get_{val.Name}")));
            }

            wrongGeneratedMethods.Should().BeEmpty();
            missingMethods.Should().BeEmpty();
            missingProperties.Should().BeEmpty();
            wrongGeneratedProperties.Should().BeEmpty();
        }
    }
}