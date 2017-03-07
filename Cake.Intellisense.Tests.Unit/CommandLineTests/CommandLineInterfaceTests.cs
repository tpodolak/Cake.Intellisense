using System.Collections.Generic;
using System.Runtime.Versioning;
using Cake.Intellisense.CommandLine;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;
using IPackageManager = Cake.Intellisense.NuGet.IPackageManager;

namespace Cake.Intellisense.Tests.Unit.CommandLineTests
{
    public partial class CommandLineInterfaceTests
    {
        public class InteractMethod : Test<CommandLineInterface>
        {
            [Fact]
            public void ReturnsNullWhenParserErrorsOccured()
            {
                var errorList = new List<ParserError>
                {
                    new ParserError()
                };

                Get<IArgumentParser>()
                    .Parse<MetadataGeneratorOptions>(Arg.Any<string[]>())
                    .Returns(new ParserResult<MetadataGeneratorOptions>(new MetadataGeneratorOptions(), errorList));

                var result = Subject.Interact(new string[0]);

                result.Should().BeNull();
                Get<IPackageManager>().DidNotReceive().FindPackage(Arg.Any<string>(), Arg.Any<string>());
            }

            [Fact]
            public void ReturnsNullWhenPackageNotFoundInNuget()
            {
                Get<IArgumentParser>()
                    .Parse<MetadataGeneratorOptions>(Arg.Any<string[]>())
                    .Returns(
                        new ParserResult<MetadataGeneratorOptions>(new MetadataGeneratorOptions(), new List<ParserError>()));

                Get<IPackageManager>().FindPackage(Arg.Any<string>(), Arg.Any<string>()).Returns((IPackage)null);

                var result = Subject.Interact(new string[0]);

                result.Should().BeNull();
                Get<IPackageManager>().Received(1).FindPackage(Arg.Any<string>(), Arg.Any<string>());
            }

            [Fact]
            public void ReturnsIntactMetadataGeneratorOptionsWhenFrameworkSpecified()
            {
                var frameworkVersion = ".NETFramework,Version=v4.5";
                var outputFolder = "outputFolder";
                var packageVersion = "packageVersion";
                var package = "package";

                Get<IArgumentParser>()
                    .Parse<MetadataGeneratorOptions>(Arg.Any<string[]>())
                    .Returns(new ParserResult<MetadataGeneratorOptions>(
                        new MetadataGeneratorOptions
                        {
                            OutputFolder = outputFolder,
                            PackageVersion = packageVersion,
                            Package = package,
                            TargetFramework = frameworkVersion
                        }, new List<ParserError>()));

                var result = Subject.Interact(new string[0]);

                result.Should().NotBeNull();
                result.TargetFramework.Should().Be(frameworkVersion);
                result.PackageVersion.Should().Be(packageVersion);
                result.Package.Should().Be(package);
                result.OutputFolder.Should().Be(outputFolder);
            }

            [Theory]
            [InlineData("")]
            [InlineData(" ")]
            [InlineData(null)]
            public void ListsAllAvailableFrameworksWhenTargetFrameworkNotSpecified(string targetFramework)
            {
                int index = -1;
                int resultIndex = 0;
                var frameworkName = new FrameworkName(".NETFramework,Version=v4.5");
                var frameworkNames = new List<FrameworkName> { frameworkName };

                Get<IArgumentParser>()
                    .Parse<MetadataGeneratorOptions>(Arg.Any<string[]>())
                    .Returns(
                        new ParserResult<MetadataGeneratorOptions>(
                            new MetadataGeneratorOptions { TargetFramework = targetFramework },
                            new List<ParserError>()));

                Get<IPackageManager>().FindPackage(Arg.Any<string>(), Arg.Any<string>()).Returns(Use<IPackage>());
                Get<IPackageManager>().GetTargetFrameworks(Arg.Any<IPackage>()).Returns(frameworkNames);
                Get<IConsoleReader>().TryRead(out index).Returns(callInfo =>
                {
                    callInfo[0] = 0;
                    return true;
                });
                var result = Subject.Interact(new string[0]);

                result.Should().NotBeNull();
                result.TargetFramework.Should().Be(frameworkName.FullName);
                Get<IConsoleReader>().Received(1).TryRead(out resultIndex);
            }
        }
    }
}