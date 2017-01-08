using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cake.MetadataGenerator.CommandLine;
using FluentAssertions;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Integration
{
    public class MetadataGeneratorTests
    {
        private readonly MetadataGenerator generator;

        public MetadataGeneratorTests()
        {
            generator = new MetadataGenerator();
        }

        [Fact]
        public void GenerateCanGenerateMetadataBasedOnCakeCommonLibTests()
        {
            var options = CreateMetadataGeneratorOptions("Cake.Common", ".NETFramework,Version=v4.5");
            var result = generator.Generate(options);
            result.Should().NotBeNull();
            result.SourceAssemblies.Should().HaveCount(count => count > 0);
            result.EmitedAssembly.Should().NotBeNull();
            VerifyAssemblyContent(result.SourceAssemblies, result.EmitedAssembly);
        }

        private void VerifyAssemblyContent(Assembly[] sourceAssemblies, Assembly emitedAssembly)
        {
            var sourceTypes = sourceAssemblies.SelectMany(val => val.GetExportedTypes())
                                              .Where(val => val.GetCustomAttributes().Any(attr => attr.GetType().FullName == "Cake.Core.Annotations.CakeAliasCategoryAttribute"))
                                              .ToList();

            var emitedTypes = emitedAssembly.GetExportedTypes().ToDictionary(key => key.FullName);

            var generatedTypes = sourceTypes.Where(val => emitedTypes.ContainsKey(val.FullName + "Metadata")).Select(val => emitedTypes[val.FullName + "Metadata"]).ToList();

            generatedTypes.Should().HaveSameCount(sourceTypes);
            generatedTypes.All(type => type.IsPublic).Should().BeTrue();

            var wrongGeneratedMethods = new Dictionary<MethodInfo, List<MethodInfo>>();
            foreach (var sourceType in sourceTypes)
            {
                var generatedType = emitedTypes[sourceType.FullName + "Metadata"];
                var sourceMethods = sourceType.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(method => method.GetCustomAttributes().Any(attr => attr.GetType().FullName == "Cake.Core.Annotations.CakeMethodAliasAttribute"))
                                              .ToList();
                var emitedMethods = generatedType.GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();

                foreach (var methodInfo in sourceMethods.Where(sourceMethod => !emitedMethods.Any(val => SameAliasMethod(sourceMethod, val))))
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
    }
}
