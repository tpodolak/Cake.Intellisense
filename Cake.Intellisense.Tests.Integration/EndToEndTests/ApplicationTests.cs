using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cake.Intellisense.CodeGeneration.MetadataGenerators;
using Cake.Intellisense.Tests.Integration.Extensions;
using Xunit;

namespace Cake.Intellisense.Tests.Integration.EndToEndTests
{
    public class ApplicationTests
    {
        public class RunMethod
        {
            private const string DefaultFramework = ".NETFramework,Version=v4.5";
            private readonly Application _application = new Application();

            public static IEnumerable<object[]> CakePackages
            {
                get
                {
                    yield return new object[] { "Cake.Common", DefaultFramework, "0.19.1" };
                    
                    // TODO Get rid of loading assembly to AppDomain
                    // yield return new object[] { "Cake.Powershell", DefaultFramework, "0.3.0" };
                }
            }

            public static IEnumerable<object[]> CakeCorePackages
            {
                get
                {
                    yield return new object[] { DefaultFramework, "0.19.1" };
                }
            }

            [Theory]
            [MemberData(nameof(CakePackages))]
            public void CanGenerateMetadataForPackageTest(string package, string framework, string version)
            {
                var options = CreateMetadataGeneratorOptions(package, framework, version);

                var result = _application.Run(options);

                PrepareAssemblies(result);
                result.Should().NotBeNull().And.GenerateValidCakeAssemblies();
            }

            [Theory]
            [MemberData(nameof(CakeCorePackages))]
            public void CanGenerateMetadataForCakeCoreLibTest(string framework, string version)
            {
                var options = CreateMetadataGeneratorOptions("Cake.Core", framework, version);

                var result = _application.Run(options);

                PrepareAssemblies(result);
                result.Should().NotBeNull().And.GenerateValidCakeCoreAssemblies();
            }

            private string[] CreateMetadataGeneratorOptions(string package, string targetFramework, string version)
            {
                return new[]
                {
                    "--Package", package, "--PackageVersion", version ?? string.Empty, "--TargetFramework", targetFramework,
                    "--OutputFolder", Path.Combine(Environment.CurrentDirectory, "Result")
                };
            }

            private void PrepareAssemblies(GeneratorResult result)
            {
                var referencedAssemblies = result?.EmitedAssemblies
                                               .SelectMany(assembly => assembly.GetReferencedAssemblies()
                                                   .Select(val => val.FullName))
                                               .ToList() ?? new List<string>();

                // TODO get rid of manual coping and assembly loading
                var locations = AppDomain.CurrentDomain
                                         .GetAssemblies()
                                         .Where(assembly => referencedAssemblies.Contains(assembly.FullName))
                                         .Select(assembly => assembly.Location);

                foreach (var val in locations)
                {
                    File.Copy(val, Path.Combine(Environment.CurrentDirectory, Path.GetFileName(val)), true);
                }
            }
        }
    }
}
