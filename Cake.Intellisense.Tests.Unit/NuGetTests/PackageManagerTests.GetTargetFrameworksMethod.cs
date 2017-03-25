using System.Runtime.Versioning;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.NuGetTests
{
    public partial class PackageManagerTests
    {
        public class GetTargetFrameworksMethod : Test<NuGet.PackageManager>
        {
            public GetTargetFrameworksMethod()
            {
                Use<IPackage>();
            }

            [Fact]
            public void DeducesTargetFrameworkBasedOnAssemblyReferences()
            {
                Get<IPackage>().AssemblyReferences.Returns(new[] { Use<IPackageAssemblyReference>() });
                Get<IPackageAssemblyReference>().TargetFramework.Returns(new FrameworkName(".NETFramework,Version=v4.5"));

                var result = Subject.GetTargetFrameworks(Get<IPackage>());

                result.Should().NotBeNull();
                result.Should().HaveCount(1);
                result.Should().ContainSingle(framework => framework.FullName == ".NETFramework,Version=v4.5");
            }
        }
    }
}