using System.Linq;
using System.Reflection;
using Cake.Intellisense.CommandLine;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.CommandLineTests
{
    public class HelpScreenGeneratorTests
    {
        public class GenerateMethod : Test<HelpScreenGenerator>
        {
            [Fact]
            public void GeneratesProperHelpsString_ForMetadataGeneratorOptions()
            {
                var assembly = typeof(MetadataGeneratorOptions).Assembly;
                var assemblyAttributes = assembly.GetCustomAttributes().ToList();
                var assemblyTitle = assemblyAttributes.OfType<AssemblyTitleAttribute>().Single().Title;
                var assemblyCopyright = assemblyAttributes.OfType<AssemblyCopyrightAttribute>().Single().Copyright;
                var assemblyVersion = assemblyAttributes.OfType<AssemblyInformationalVersionAttribute>().Single().InformationalVersion;

                var result = Subject.Generate<MetadataGeneratorOptions>();

                result.Should().Be($@"{assemblyTitle} {assemblyVersion}
{assemblyCopyright}

  --Package            Required. Cake or Cake addin package

  --PackageVersion     Package version

  --OutputFolder       Output folder for generated libraries

  --TargetFramework    Package target framework

");
            }
        }
    }
}