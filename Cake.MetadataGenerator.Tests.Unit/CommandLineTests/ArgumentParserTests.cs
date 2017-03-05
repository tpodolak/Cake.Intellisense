using Cake.MetadataGenerator.CommandLine;
using Cake.MetadataGenerator.Tests.Unit.Common;
using FluentAssertions;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.CommandLineTests
{
    public partial class ArgumentParserTests
    {
        public class ParseMethod : Test<ArgumentParser>
        {
            [Fact]
            public void CanParseAllMetadataGeneratorOptions()
            {
                var expectedResult = new MetadataGeneratorOptions
                {
                    OutputFolder = @"C:\Temp",
                    PackageVersion = "0.17.0",
                    Package = "Cake.Common",
                    TargetFramework = ".NETFramework,Version=v4.5"
                };

                var parserResult = Subject.Parse<MetadataGeneratorOptions>(new[]
                {
                "--Package","Cake.Common",
                "--PackageVersion", "0.17.0",
                "--OutputFolder", @"C:\Temp",
                "--TargetFramework",".NETFramework,Version=v4.5"
            });

                parserResult.Should().NotBeNull();
                parserResult.Errors.Should().BeEmpty();
                parserResult.Result.Should().NotBeNull();
                parserResult.Result.ShouldBeEquivalentTo(expectedResult);
            }

            [Fact]
            public void DoesNotReturnErrors_WhenOptionalArgumentsMissing()
            {
                var expectedResult = new MetadataGeneratorOptions
                {
                    Package = @"Cake.Common"
                };

                var parserResult = Subject.Parse<MetadataGeneratorOptions>(new[]
                {
                    "--Package", "Cake.Common"
                });

                parserResult.Should().NotBeNull();
                parserResult.Errors.Should().BeEmpty();
                parserResult.Result.Should().NotBeNull();
                parserResult.Result.ShouldBeEquivalentTo(expectedResult);
            }

            [Fact]
            public void ReturnsErrors_WhenRequiredArgumentsMissing()
            {
                var parserResult = Subject.Parse<MetadataGeneratorOptions>(new[]
                {
                    "--PackageVersion", "0.17.0",
                    "--OutputFolder", @"C:\Temp",
                    "--TargetFramework", ".NETFramework,Version=v4.5"
                });

                parserResult.Should().NotBeNull();
                parserResult.Errors.Should().NotBeEmpty();
                parserResult.Result.Should().BeNull();
            }
        }
    }
}