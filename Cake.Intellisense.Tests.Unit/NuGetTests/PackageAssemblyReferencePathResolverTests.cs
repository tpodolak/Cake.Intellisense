using Cake.Intellisense.NuGet;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.NuGetTests
{
    public class PackageAssemblyReferencePathResolverTests
    {
        public class GetPathMethod : Test<PackageAssemblyReferencePathResolver>
        {
            [Fact]
            public void ResolvesPath_BasedOnInstallPath_AndPackageReferencePath()
            {
                Get<IPackagePathResolver>().GetInstallPath(Arg.Any<IPackage>()).Returns(@"C:\Temp");
                Use<IPackageAssemblyReference>().Path.Returns("NuGet");

                var result = Subject.GetPath(Use<IPackage>(), Get<IPackageAssemblyReference>());

                result.Should().Be(@"C:\Temp\NuGet");
            }
        }
    }
}