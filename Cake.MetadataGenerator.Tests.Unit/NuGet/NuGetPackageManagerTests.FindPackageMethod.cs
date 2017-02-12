using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.NuGet;
using Cake.MetadataGenerator.Settings;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.NuGet
{
    public partial class NuGetPackageManagerTests
    {
        public class FindPackageMethod : Test<NuGetPackageManager>
        {
            public FindPackageMethod()
            {
                Get<INugetPackageRepositoryProvider>().Get().Returns(Use<IPackageRepository>());
                Get<INugetPackageManagerProvider>().Get().Returns(Use<IPackageManager>());
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

            [Theory]
            [InlineData(false, "1.0.0.0")]
            [InlineData(true, "2.0.0-special")]
            public void ReturnsLastVersionWhenVersionNotSpecifiedAndPrereleasePackagesBlocked(bool allowPreRelease, string expectedVersion)
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
        }
    }
}