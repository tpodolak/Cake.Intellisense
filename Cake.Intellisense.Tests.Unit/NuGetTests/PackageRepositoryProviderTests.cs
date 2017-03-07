using Cake.Intellisense.Logging;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.Settings;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.NuGetTests
{
    public class PackageRepositoryProviderTests
    {
        public class GetMethod : Test<PackageRepositoryProvider>
        {
            [Fact]
            public void InitializesPackageRepositoryWithProperPackageSource()
            {
                var packageSource = "https://packages.nuget.org/api/v2";
                Get<INuGetSettings>().PackageSource.Returns(packageSource);

                Subject.Get();

                Get<IPackageRepositoryFactory>().Received(1).CreateRepository(Arg.Is<string>(val => val == packageSource));
            }

            [Fact]
            public void InitializesPackageRepositoryWithNuGetNLogAdapter()
            {
                Get<IPackageRepositoryFactory>().CreateRepository(Arg.Any<string>()).Returns(Use<PackageRepositoryBase>());

                var result = Subject.Get();

                var repo = result.As<PackageRepositoryBase>();
                repo.Logger.Should().BeOfType<NLogNugetLoggerAdapter>();
            }
        }
    }
}