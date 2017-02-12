﻿using System.Collections.Generic;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.CommandLine;
using Cake.MetadataGenerator.NuGet;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.CommandLineTests
{
    public class CommandLineInterfaceTests : Test<CommandLineInterface>
    {
        [Fact]
        public void InteractReturnsNullWhenParserErrorsOccured()
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
            Get<INuGetPackageManager>().DidNotReceive().FindPackage(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void InteractReturnsNullWhenPackageNotFoundInNuget()
        {
            Get<IArgumentParser>()
                .Parse<MetadataGeneratorOptions>(Arg.Any<string[]>())
                .Returns(
                    new ParserResult<MetadataGeneratorOptions>(new MetadataGeneratorOptions(),
                        new List<ParserError>()));

            Get<INuGetPackageManager>().FindPackage(Arg.Any<string>(), Arg.Any<string>()).Returns((IPackage)null);

            var result = Subject.Interact(new string[0]);

            result.Should().BeNull();
            Get<INuGetPackageManager>().Received(1).FindPackage(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void InteractReturnsIntactMetadataGeneratorOptionsWhenFrameworkSpecified()
        {
            var frameworkVersion = ".NETFramework,Version=v4.5";
            var outputFolder = "outputFolder";
            var packageVersion = "packageVersion";
            var package = "package";

            Get<IArgumentParser>()
                .Parse<MetadataGeneratorOptions>(Arg.Any<string[]>())
                .Returns(
                    new ParserResult<MetadataGeneratorOptions>(new MetadataGeneratorOptions
                    {
                        OutputFolder = outputFolder,
                        PackageVersion = packageVersion,
                        Package = package,
                        TargetFramework = frameworkVersion
                    },
                        new List<ParserError>()));

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
        public void InteractListsAllAvilableFrameworksWhenTargetFrameworkNotSpecified(string targetFramework)
        {
            int index = -1;
            int resultIndex = 0;
            var frameworkName = new FrameworkName(".NETFramework,Version=v4.5");
            var frameworkNames = new List<FrameworkName> { frameworkName };

            Get<IArgumentParser>()
               .Parse<MetadataGeneratorOptions>(Arg.Any<string[]>())
               .Returns(
                   new ParserResult<MetadataGeneratorOptions>(new MetadataGeneratorOptions { TargetFramework = targetFramework },
                       new List<ParserError>()));

            Get<INuGetPackageManager>().FindPackage(Arg.Any<string>(), Arg.Any<string>()).Returns(Use<IPackage>());
            Get<INuGetPackageManager>().GetTargetFrameworks(Arg.Any<IPackage>()).Returns(frameworkNames);
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