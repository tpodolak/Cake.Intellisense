using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters.Interfaces;
using Cake.Intellisense.Compilation;
using Cake.Intellisense.Compilation.Interfaces;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.NuGet.Interfaces;
using Cake.Intellisense.Reflection;
using Cake.Intellisense.Reflection.Interfaces;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NSubstitute;
using NuGet;
using Xunit;
using IDependencyResolver = Cake.Intellisense.NuGet.Interfaces.IDependencyResolver;
using IPackageManager = Cake.Intellisense.NuGet.Interfaces.IPackageManager;

namespace Cake.Intellisense.Tests.Unit
{
    public class MetadataGeneratorTests
    {
        public class GenerateMethod : Test<MetadataGenerator>
        {
            public GenerateMethod()
            {
                Get<IPackageManager>().InstallPackage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<FrameworkName>())
                                      .Returns(Use<IPackage>());
                Get<IDependencyResolver>()
                    .GetDependentPackagesAndSelf(Arg.Any<IPackage>(), Arg.Any<FrameworkName>())
                    .Returns(new List<IPackage>());
            }

            [Fact]
            public void ReturnsNull_WhenNugetPackageNotFound()
            {
                Get<IPackageManager>().InstallPackage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<FrameworkName>())
                                      .Returns((IPackage)null);

                var metadataGeneratorOptions = new MetadataGeneratorOptions
                {
                    PackageVersion = "0.17.0",
                    Package = "Cake.Common",
                    TargetFramework = ".NETFramework,Version=v4.5"
                };

                var result = Subject.Generate(metadataGeneratorOptions);

                result.Should().BeNull();
                Get<IPackageManager>()
                    .Received(1)
                    .InstallPackage(
                        Arg.Is<string>(val => val == metadataGeneratorOptions.Package),
                        Arg.Is<string>(val => val == metadataGeneratorOptions.PackageVersion),
                        Arg.Is<FrameworkName>(val => val.FullName == metadataGeneratorOptions.TargetFramework));
            }

            [Fact]
            public void EmitsRewrittenAssemblyForEachPackageSourceAssembly()
            {
                var firstAssembly = typeof(object).Assembly;
                var secondAssembly = typeof(Stack<>).Assembly;
                var firstCompilation = Substitute.For<Microsoft.CodeAnalysis.Compilation>("mscorlib.Metadata", null, null, false, null);
                var secondCompilation = Substitute.For<Microsoft.CodeAnalysis.Compilation>("System.Metadata", null, null, false, null);
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
                Get<ICompilationProvider>().Get(Arg.Is<string>(assemblyName => assemblyName == "mscorlib.Metadata"), Arg.Any<IEnumerable<SyntaxTree>>(), Arg.Any<IEnumerable<MetadataReference>>(), Arg.Any<CSharpCompilationOptions>())
                                           .Returns(firstCompilation);
                Get<ICompilationProvider>().Get(Arg.Is<string>(assemblyName => assemblyName == "System.Metadata"), Arg.Any<IEnumerable<SyntaxTree>>(), Arg.Any<IEnumerable<MetadataReference>>(), Arg.Any<CSharpCompilationOptions>())
                                           .Returns(secondCompilation);
                Get<ICompiler>().Compile(Arg.Is<Microsoft.CodeAnalysis.Compilation>(val => val.AssemblyName == "mscorlib.Metadata"), Arg.Any<string>()).Returns(secondAssembly);
                Get<ICompiler>().Compile(Arg.Is<Microsoft.CodeAnalysis.Compilation>(val => val.AssemblyName == "System.Metadata"), Arg.Any<string>()).Returns(firstAssembly);

                var result = Subject.Generate(new MetadataGeneratorOptions { TargetFramework = ".NETFramework,Version=v4.5" });

                result.SourceAssemblies[0].Should().BeSameAs(firstAssembly);
                result.SourceAssemblies[1].Should().BeSameAs(secondAssembly);
                result.EmitedAssemblies[0].Should().BeSameAs(secondAssembly);
                result.EmitedAssemblies[1].Should().BeSameAs(firstAssembly);
                Get<ICompiler>().Received(1).Compile(Arg.Is<Microsoft.CodeAnalysis.Compilation>(val => val.AssemblyName == "mscorlib.Metadata"), Arg.Is<string>(val => val == "mscorlib.Metadata.dll"));
                Get<ICompiler>().Received(1).Compile(Arg.Is<Microsoft.CodeAnalysis.Compilation>(val => val.AssemblyName == "System.Metadata"), Arg.Is<string>(val => val == "System.Metadata.dll"));
            }

            [Fact]
            public void LoadMetadataReferencesFromAllDependentPackages()
            {
                var frameworkVersion = ".NETFramework,Version=v4.5";
                var packageFile = Use<IPackageFile>();
                var firstAssembly = typeof(object).Assembly;
                var assemblies = new List<Assembly>
                {
                    firstAssembly
                };

                packageFile.SupportedFrameworks.Returns(new List<FrameworkName> { new FrameworkName(frameworkVersion) });

                Get<IMetadataReferenceLoader>().CreateFromFile(Arg.Any<string>()).Returns(MetadataReference.CreateFromStream(new MemoryStream()));

                Get<IPackageAssemblyResolver>().ResolveAssemblies(Arg.Any<IPackage>(), Arg.Any<FrameworkName>())
                                               .Returns(assemblies);
                Get<ICakeSyntaxRewriterService>().Rewrite(Arg.Any<CompilationUnitSyntax>(), Arg.Any<Assembly>())
                                                 .Returns(CSharpSyntaxTree.ParseText(string.Empty).GetRoot());
                Get<IDependencyResolver>().GetDependentPackagesAndSelf(Arg.Any<IPackage>(), Arg.Any<FrameworkName>())
                                          .Returns(new List<IPackage> { Get<IPackage>() });

                Subject.Generate(new MetadataGeneratorOptions { TargetFramework = frameworkVersion });

                Get<IMetadataReferenceLoader>()
                    .Received(1)
                    .CreateFromPackages(Arg.Is<IList<IPackage>>(list => list.Count == 1 && list[0] == Get<IPackage>()), Arg.Is<FrameworkName>(framework => framework.FullName == frameworkVersion));
            }
        }
    }
}