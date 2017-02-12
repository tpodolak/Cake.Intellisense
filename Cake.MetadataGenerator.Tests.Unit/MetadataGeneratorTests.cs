﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters;
using Cake.MetadataGenerator.NuGet;
using Cake.MetadataGenerator.Reflection;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit
{
    public class MetadataGeneratorTests : Test<MetadataGenerator>
    {
        public MetadataGeneratorTests()
        {
            Get<INuGetPackageManager>().InstallPackage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<FrameworkName>())
                              .Returns(Use<IPackage>());
            Get<INuGetDependencyResolver>()
                .GetDependentPackagesAndSelf(Arg.Any<IPackage>(), Arg.Any<FrameworkName>())
                .Returns(new List<IPackage>());
        }

        [Fact]
        public void GenerateReturnsNullWhenNugetPackageNotFound()
        {
            Get<INuGetPackageManager>().InstallPackage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<FrameworkName>())
                                          .Returns((IPackage)null);

            var metadataGeneratorOptions = new MetadataGeneratorOptions
            {
                PackageVersion = "0.17.0",
                Package = "Cake.Common",
                TargetFramework = ".NETFramework,Version=v4.5"
            };

            var result = Subject.Generate(metadataGeneratorOptions);

            result.Should().BeNull();
            Get<INuGetPackageManager>()
                .Received(1)
                .InstallPackage(Arg.Is<string>(val => val == metadataGeneratorOptions.Package),
                    Arg.Is<string>(val => val == metadataGeneratorOptions.PackageVersion),
                    Arg.Is<FrameworkName>(val => val.FullName == metadataGeneratorOptions.TargetFramework));
        }

        [Fact]
        public void GenerateEmitsRewrittenAssemblyForEachPackageSourceAssembly()
        {
            var firstAssembly = typeof(object).Assembly;
            var secondAssembly = typeof(Stack<>).Assembly;
            var assemblies = new List<Assembly>
            {
                firstAssembly,
                secondAssembly
            };

            Get<IMetadataReferenceLoader>().CreateFromFile(Arg.Any<string>()).Returns(MetadataReference.CreateFromStream(new MemoryStream()));
            Get<IPackageAssemblyResolver>().ResolveAssemblies(Arg.Any<IPackage>(), Arg.Any<FrameworkName>())
                                              .Returns(assemblies);
            Get<ICakeSyntaxRewriterService>().Rewrite(Arg.Any<CompilationUnitSyntax>(), Arg.Any<Assembly>())
                                                .Returns(CSharpSyntaxTree.ParseText(string.Empty).GetRoot());

            Get<ICompiler>().Compile(Arg.Is<CSharpCompilation>(val => val.AssemblyName == "mscorlib.Metadata"), Arg.Any<string>()).Returns(secondAssembly);
            Get<ICompiler>().Compile(Arg.Is<CSharpCompilation>(val => val.AssemblyName == "System.Metadata"), Arg.Any<string>()).Returns(firstAssembly);

            var result = Subject.Generate(new MetadataGeneratorOptions { TargetFramework = ".NETFramework,Version=v4.5" });

            result.SourceAssemblies[0].Should().BeSameAs(firstAssembly);
            result.SourceAssemblies[1].Should().BeSameAs(secondAssembly);
            result.EmitedAssemblies[0].Should().BeSameAs(secondAssembly);
            result.EmitedAssemblies[1].Should().BeSameAs(firstAssembly);
            Get<ICompiler>().Received(1).Compile(Arg.Is<CSharpCompilation>(val => val.AssemblyName == "mscorlib.Metadata"), Arg.Is<string>(val => val == "mscorlib.Metadata.dll"));
            Get<ICompiler>().Received(1).Compile(Arg.Is<CSharpCompilation>(val => val.AssemblyName == "System.Metadata"), Arg.Is<string>(val => val == "System.Metadata.dll"));
        }

        [Fact(Skip = "Looks like reading assembly from stream remove info about location")]
        public void GenerateLoadMetadataReferencesFromAllAssembliesAndReferencedAssemblies()
        {
            var frameworkVersion = ".NETFramework,Version=v4.5";
            var packageFile = Use<IPackageFile>();
            var firstAssembly = typeof(object).Assembly;
            var assemblies = new List<Assembly>
            {
                firstAssembly,
            };

            packageFile.SupportedFrameworks.Returns(new List<FrameworkName> { new FrameworkName(frameworkVersion) });
            packageFile.Path.Returns("path.dll");
            Get<IPackage>().GetFiles().Returns(new List<IPackageFile> { packageFile });

            Get<IMetadataReferenceLoader>().CreateFromFile(Arg.Any<string>()).Returns(MetadataReference.CreateFromStream(new MemoryStream()));
            Get<IMetadataReferenceLoader>().CreateFromStream(Arg.Any<Stream>()).Returns(MetadataReference.CreateFromStream(new MemoryStream()));

            Get<IPackageAssemblyResolver>().ResolveAssemblies(Arg.Any<IPackage>(), Arg.Any<FrameworkName>())
                                              .Returns(assemblies);
            Get<ICakeSyntaxRewriterService>().Rewrite(Arg.Any<CompilationUnitSyntax>(), Arg.Any<Assembly>())
                                                .Returns(CSharpSyntaxTree.ParseText(string.Empty).GetRoot());
            Get<INuGetDependencyResolver>().GetDependentPackagesAndSelf(Arg.Any<IPackage>(), Arg.Any<FrameworkName>())
                                              .Returns(new List<IPackage> { Get<IPackage>() });
            Get<IAssemblyLoader>().LoadReferencedAssemblies(Arg.Any<Stream>()).Returns(assemblies);

            Get<ICompiler>().Compile(Arg.Is<CSharpCompilation>(val => val.AssemblyName == "mscorlib.Metadata"), Arg.Any<string>()).Returns(firstAssembly);

            Subject.Generate(new MetadataGeneratorOptions { TargetFramework = frameworkVersion });

            Get<ICompiler>().Received(1).Compile(Arg.Is<CSharpCompilation>(val => val.ExternalReferences.Length == 3), Arg.Any<string>());
            Get<IAssemblyLoader>().Received(1).LoadReferencedAssemblies(Arg.Any<Stream>());
        }
    }
}