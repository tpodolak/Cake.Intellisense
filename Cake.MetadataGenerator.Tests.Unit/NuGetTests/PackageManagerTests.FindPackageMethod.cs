using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.NuGet;
using Cake.MetadataGenerator.Settings;
using Cake.MetadataGenerator.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;
using IPackageManager = NuGet.IPackageManager;
using PackageManager = Cake.MetadataGenerator.NuGet.PackageManager;

namespace Cake.MetadataGenerator.Tests.Unit.NuGetTests
{
    public partial class PackageManagerTests
    {
        public class FindPackageMethod : Test<PackageManager>
        {
            public FindPackageMethod()
            {
                Use<IPackage>();
            }

            [Fact]
            public void ReturnsNull_WhenPackageNotFound()
            {
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");

                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage>().AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", string.Empty, targetFramework);

                result.Should().BeNull();
                Get<IPackageManager>().DidNotReceive().InstallPackage(Arg.Any<IPackage>(), Arg.Any<bool>(), Arg.Any<bool>());
            }

            [Theory]
            [InlineData(false, "1.0.0.0")]
            [InlineData(true, "2.0.0-special")]
            public void ReturnsLastVersion_WhenVersionNotSpecifiedAndPrereleasePackagesBlocked(bool allowPreRelease, string expectedVersion)
            {
                var firstPackage = Substitute.For<IPackage>();
                var secondPackage = Substitute.For<IPackage>();

                var packageFile = Use<IPackageFile>();
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");

                packageFile.TargetFramework.Returns(targetFramework);
                firstPackage.GetSupportedFrameworks().Returns(new[] { targetFramework });
                firstPackage.Version.Returns(new SemanticVersion(2, 0, 0, "special"));
                firstPackage.Id.Returns("Cake.Common");
                firstPackage.GetFiles().Returns(new[] { packageFile });

                secondPackage.GetSupportedFrameworks().Returns(new[] { targetFramework });
                secondPackage.Version.Returns(new SemanticVersion(1, 0, 0, 0));
                secondPackage.Id.Returns("Cake.Common");
                secondPackage.GetFiles().Returns(new[] { packageFile });

                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage> { secondPackage, firstPackage }.AsQueryable());
                Get<INuGetSettings>().AllowPreReleaseVersions.Returns(allowPreRelease);
                var result = Subject.InstallPackage("Cake.Common", string.Empty, targetFramework);

                result.Should().NotBeNull();
                result.Version.ToString().Should().Be(expectedVersion);
            }

            [Fact]
            public void ReturnsVersionTargetingGivenFramework()
            {
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");
                var firstPackage = Substitute.For<IPackage>();
                var secondPackage = Substitute.For<IPackage>();
                var firstPackageFile = Use<IPackageFile>();
                var secondPackageFile = Substitute.For<IPackageFile>();
                secondPackageFile.TargetFramework.Returns(new FrameworkName(".NETFramework,Version=v4.6"));
                firstPackageFile.TargetFramework.Returns(targetFramework);
                firstPackage.Version.Returns(new SemanticVersion(2, 0, 0, 0));
                firstPackage.Id.Returns("Cake.Common");
                firstPackage.GetFiles().Returns(new[] { firstPackageFile });

                secondPackage.Version.Returns(new SemanticVersion(1, 0, 0, 0));
                secondPackage.Id.Returns("Cake.Common");
                secondPackage.GetFiles().Returns(new[] { secondPackageFile });
                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage> { secondPackage, firstPackage }.AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", string.Empty, targetFramework);

                result.Should().NotBeNull();
                result.GetFiles().Select(val => val.TargetFramework).Should().ContainSingle(framework => framework == targetFramework);
            }

            public override object CreateInstance(Type type, params object[] constructorArgs)
            {
                if (type == typeof(IPackageRepositoryProvider))
                {
                    var nugetPackageRepositoryProvider = Substitute.For<IPackageRepositoryProvider>();
                    nugetPackageRepositoryProvider.Get().Returns(Use<IPackageRepository>());
                    return nugetPackageRepositoryProvider;
                }

                if (type == typeof(IPackageManagerProvider))
                {
                    var nugetPackageManagerProvider = Substitute.For<IPackageManagerProvider>();
                    nugetPackageManagerProvider.Get().Returns(Use<IPackageManager>());
                    return nugetPackageManagerProvider;
                }

                return base.CreateInstance(type, constructorArgs);
            }
        }
    }
}