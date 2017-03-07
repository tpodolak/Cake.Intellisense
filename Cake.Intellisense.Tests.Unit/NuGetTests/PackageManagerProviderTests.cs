using Cake.MetadataGenerator.NuGet;
using Cake.MetadataGenerator.Settings;
using Cake.MetadataGenerator.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.NuGetTests
{
    public partial class PackageManagerProviderTests
    {
        public class GetMethod : Test<PackageManagerProvider>
        {
            [Fact]
            public void ReturnsPackageManagerWithProperLocalRepositoryPath()
            {
                var packageRepository = Use<IPackageRepository>();
                var localRepositoryPath = @"C:\Temp";
                Get<INuGetSettings>().LocalRepositoryPath.Returns(localRepositoryPath);
                Get<IPackageRepositoryProvider>().Get().Returns(packageRepository);

                var packageManager = Subject.Get();

                packageManager.SourceRepository.Should().BeSameAs(packageRepository);
                var repositoryPath = Get<INuGetSettings>().Received(1).LocalRepositoryPath;
            }
        }
    }
}