using System.Runtime.Versioning;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.NuGet.Interfaces;
using Cake.Intellisense.Reflection;
using Cake.Intellisense.Reflection.Interfaces;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.NuGetTests
{
    public partial class PackageAssemblyResolverTests : Test<PackageAssemblyResolver>
    {
        public class ResolveAssemblies : Test<PackageAssemblyResolver>
        {
            [Fact]
            public void ResolveAssemblies_RetrievesPackageAssemblies_BasedOnAssemblyReferencesSupportingGivenFramework()
            {
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");
                var otherFramework = new FrameworkName(".NETStandard, Version=1.6");
                var firstPackageReference = Substitute.For<IPackageAssemblyReference>();
                var secondPackageReference = Substitute.For<IPackageAssemblyReference>();
                firstPackageReference.SupportedFrameworks.Returns(new[] { targetFramework });
                secondPackageReference.SupportedFrameworks.Returns(new[] { otherFramework });

                Get<IPackageAssemblyReferencePathResolver>().GetPath(Arg.Any<IPackage>(), Arg.Is<IPackageAssemblyReference>(assemblyRef => assemblyRef == firstPackageReference))
                                                            .Returns("firstAssembly");

                Get<IPackageAssemblyReferencePathResolver>().GetPath(Arg.Any<IPackage>(), Arg.Is<IPackageAssemblyReference>(assemblyRef => assemblyRef == secondPackageReference))
                                                            .Returns("secondAssembly");

                Get<IAssemblyLoader>().LoadFrom(Arg.Is<string>(path => path == "firstAssembly")).Returns(GetType().Assembly);
                Get<IAssemblyLoader>().LoadFrom(Arg.Is<string>(path => path == "secondAssembly")).Returns(typeof(string).Assembly);

                Use<IPackage>().AssemblyReferences.Returns(new[] { secondPackageReference, firstPackageReference });

                var result = Subject.ResolveAssemblies(Get<IPackage>(), targetFramework);

                result.Should().HaveCount(1);
                result.Should().ContainSingle(assembly => assembly == GetType().Assembly);
                Get<IPackageAssemblyReferencePathResolver>().Received(1).GetPath(Arg.Any<IPackage>(), Arg.Any<IPackageAssemblyReference>());
                Get<IAssemblyLoader>().Received(1).LoadFrom(Arg.Any<string>());
            }

            [Fact]
            public void ResolveAssemblies_RetrievesPackageAssemblies_FromPoperPath()
            {
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");
                var firstPackageReference = Use<IPackageAssemblyReference>();
                firstPackageReference.SupportedFrameworks.Returns(new[] { targetFramework });

                Get<IPackageAssemblyReferencePathResolver>().GetPath(Arg.Any<IPackage>(), Arg.Is<IPackageAssemblyReference>(assemblyRef => assemblyRef == firstPackageReference))
                                                            .Returns("firstAssembly");

                Use<IPackage>().AssemblyReferences.Returns(new[] { firstPackageReference });

                Subject.ResolveAssemblies(Get<IPackage>(), targetFramework);

                Get<IPackageAssemblyReferencePathResolver>()
                    .Received(1)
                    .GetPath(Arg.Is<IPackage>(package => package == Get<IPackage>()), Arg.Is<IPackageAssemblyReference>(assembly => assembly == firstPackageReference));
            }
        }
    }
}