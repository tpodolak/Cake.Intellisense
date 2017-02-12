using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.NuGet;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.NuGet
{
    public partial class NuGetPackageManagerTests
    {
        public class InstallPackageMethod : Test<NuGetPackageManager>
        {
            public InstallPackageMethod()
            {
                Use<IPackage>();
            }

            [Fact]
            public void ReturnsNullWhenPackageNotFound()
            {
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");

                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage>().AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", string.Empty, targetFramework);

                result.Should().BeNull();
                Get<IPackageManager>().DidNotReceive().InstallPackage(Arg.Any<IPackage>(), Arg.Any<bool>(), Arg.Any<bool>());
            }

            [Fact]
            public void ReturnsAndInstallPackageWhenPackageFoundAndTargetsGivenTargetFramework()
            {
                var packageFile = Use<IPackageFile>();
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");
                packageFile.TargetFramework.Returns(targetFramework);
                Get<IPackage>().GetSupportedFrameworks().Returns(new[] { targetFramework });
                Get<IPackage>().Version.Returns(new SemanticVersion(1, 0, 0, 0));
                Get<IPackage>().Id.Returns("Cake.Common");
                Get<IPackage>().GetFiles().Returns(new[] { packageFile });
                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage> { Get<IPackage>() }.AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", string.Empty, targetFramework);

                result.Should().NotBeNull();
                Get<IPackageManager>().Received(1).InstallPackage(Arg.Is<IPackage>(val => val == Get<IPackage>()), Arg.Is<bool>(val => val == false), Arg.Any<bool>());
            }

            [Fact]
            public void ReturnsNullWhenPackageFoundAndDoesNotTargetsGivenTargetFramework()
            {
                var packageFile = Use<IPackageFile>();
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");
                packageFile.TargetFramework.Returns(new FrameworkName(".NETFramework,Version=v4.6"));
                Get<IPackage>().GetSupportedFrameworks().Returns(new[] { targetFramework });
                Get<IPackage>().Version.Returns(new SemanticVersion(1, 0, 0, 0));
                Get<IPackage>().Id.Returns("Cake.Common");
                Get<IPackage>().GetFiles().Returns(new[] { packageFile });
                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage> { Get<IPackage>() }.AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", string.Empty, targetFramework);

                result.Should().BeNull();
                Get<IPackageManager>().DidNotReceive().InstallPackage(Arg.Any<IPackage>(), Arg.Any<bool>(), Arg.Any<bool>());
            }

            [Fact]
            public void ReturnsPackageForSpecificVersionWhenVersionSpecified()
            {
                var firstPackage = Substitute.For<IPackage>();
                var secondPackage = Substitute.For<IPackage>();

                var packageFile = Use<IPackageFile>();
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");

                packageFile.TargetFramework.Returns(targetFramework);
                firstPackage.GetSupportedFrameworks().Returns(new[] { targetFramework });
                firstPackage.Version.Returns(new SemanticVersion(1, 0, 0, 0));
                firstPackage.Id.Returns("Cake.Common");
                firstPackage.GetFiles().Returns(new[] { packageFile });

                secondPackage.GetSupportedFrameworks().Returns(new[] { targetFramework });
                secondPackage.Version.Returns(new SemanticVersion(2, 0, 0, 0));
                secondPackage.Id.Returns("Cake.Common");
                secondPackage.GetFiles().Returns(new[] { packageFile });

                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage> { firstPackage, secondPackage }.AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", "2.0.0.0", targetFramework);

                result.Should().NotBeNull();
                result.Version.Should().Be(new SemanticVersion(2, 0, 0, 0));
                Get<IPackageManager>().Received().InstallPackage(Arg.Any<IPackage>(), Arg.Any<bool>(), Arg.Any<bool>());
            }

            public override object CreateInstance(Type type, params object[] constructorArgs)
            {
                if (type == typeof(INugetPackageRepositoryProvider))
                {
                    var nugetPackageRepositoryProvider = Substitute.For<INugetPackageRepositoryProvider>();
                    nugetPackageRepositoryProvider.Get().Returns(Use<IPackageRepository>());
                    return nugetPackageRepositoryProvider;
                }

                if (type == typeof(INugetPackageManagerProvider))
                {
                    var nugetPackageManagerProvider = Substitute.For<INugetPackageManagerProvider>();
                    nugetPackageManagerProvider.Get().Returns(Use<IPackageManager>());
                    return nugetPackageManagerProvider;
                }
                return base.CreateInstance(type, constructorArgs);
            }
        }
    }
}