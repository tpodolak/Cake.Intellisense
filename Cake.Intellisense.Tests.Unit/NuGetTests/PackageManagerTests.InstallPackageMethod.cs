using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;
using IPackageManager = NuGet.IPackageManager;

namespace Cake.Intellisense.Tests.Unit.NuGetTests
{
    public partial class PackageManagerTests
    {
        public class InstallPackageMethod : Test<NuGet.PackageManager>
        {
            public InstallPackageMethod()
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

            [Fact]
            public void ReturnsAndInstallPackage_WhenPackageFoundAndTargetsGivenTargetFramework()
            {
                var packageAssemblyReference = Use<IPackageAssemblyReference>();
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");
                packageAssemblyReference.TargetFramework.Returns(targetFramework);
                Get<IPackage>().GetSupportedFrameworks().Returns(new[] { targetFramework });
                Get<IPackage>().Version.Returns(new SemanticVersion(1, 0, 0, 0));
                Get<IPackage>().Id.Returns("Cake.Common");
                Get<IPackage>().AssemblyReferences.Returns(new[] { packageAssemblyReference });
                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage> { Get<IPackage>() }.AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", string.Empty, targetFramework);

                result.Should().NotBeNull();
                Get<IPackageManager>().Received(1).InstallPackage(Arg.Is<IPackage>(val => val == Get<IPackage>()), Arg.Is<bool>(val => val == false), Arg.Any<bool>());
            }

            [Fact]
            public void ReturnsNull_WhenPackageFoundAndDoesNotTargetsGivenTargetFramework()
            {
                var packageAssemblyReference = Use<IPackageAssemblyReference>();
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");
                packageAssemblyReference.TargetFramework.Returns(new FrameworkName(".NETFramework,Version=v4.6"));
                Get<IPackage>().GetSupportedFrameworks().Returns(new[] { targetFramework });
                Get<IPackage>().Version.Returns(new SemanticVersion(1, 0, 0, 0));
                Get<IPackage>().Id.Returns("Cake.Common");
                Get<IPackage>().AssemblyReferences.Returns(new[] { packageAssemblyReference });
                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage> { Get<IPackage>() }.AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", string.Empty, targetFramework);

                result.Should().BeNull();
                Get<IPackageManager>().DidNotReceive().InstallPackage(Arg.Any<IPackage>(), Arg.Any<bool>(), Arg.Any<bool>());
            }

            [Fact]
            public void ReturnsPackageForSpecificVersion_WhenVersionSpecified()
            {
                var firstPackage = Substitute.For<IPackage>();
                var secondPackage = Substitute.For<IPackage>();

                var packageAssemblyReference = Use<IPackageAssemblyReference>();
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");

                packageAssemblyReference.TargetFramework.Returns(targetFramework);
                firstPackage.GetSupportedFrameworks().Returns(new[] { targetFramework });
                firstPackage.Version.Returns(new SemanticVersion(1, 0, 0, 0));
                firstPackage.Id.Returns("Cake.Common");
                firstPackage.AssemblyReferences.Returns(new[] { packageAssemblyReference });

                secondPackage.GetSupportedFrameworks().Returns(new[] { targetFramework });
                secondPackage.Version.Returns(new SemanticVersion(2, 0, 0, 0));
                secondPackage.Id.Returns("Cake.Common");
                secondPackage.AssemblyReferences.Returns(new[] { packageAssemblyReference });

                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage> { firstPackage, secondPackage }.AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", "2.0.0.0", targetFramework);

                result.Should().NotBeNull();
                result.Should().BeSameAs(secondPackage);
                Get<IPackageManager>().Received().InstallPackage(Arg.Any<IPackage>(), Arg.Any<bool>(), Arg.Any<bool>());
            }

            [Fact]
            public void ReturnsVersionTargetingGivenFramework_WhenVersionNotSpecified()
            {
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");
                var firstPackage = Substitute.For<IPackage>();
                var secondPackage = Substitute.For<IPackage>();
                var firstPackageAssemblyReference = Use<IPackageAssemblyReference>();
                var secondPackageAssemblyReference = Substitute.For<IPackageAssemblyReference>();
                secondPackageAssemblyReference.TargetFramework.Returns(new FrameworkName(".NETFramework,Version=v4.6"));
                firstPackageAssemblyReference.TargetFramework.Returns(targetFramework);
                firstPackage.Version.Returns(new SemanticVersion(2, 0, 0, 0));
                firstPackage.Id.Returns("Cake.Common");
                firstPackage.AssemblyReferences.Returns(new[] { firstPackageAssemblyReference });

                secondPackage.Version.Returns(new SemanticVersion(1, 0, 0, 0));
                secondPackage.Id.Returns("Cake.Common");
                secondPackage.AssemblyReferences.Returns(new[] { secondPackageAssemblyReference });
                Get<IPackageRepository>().GetPackages().Returns(new List<IPackage> { firstPackage, secondPackage }.AsQueryable());

                var result = Subject.InstallPackage("Cake.Common", string.Empty, targetFramework);

                result.Should().BeSameAs(firstPackage);
            }
        }
    }
}