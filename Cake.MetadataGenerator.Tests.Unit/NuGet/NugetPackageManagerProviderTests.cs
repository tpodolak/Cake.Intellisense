using Cake.MetadataGenerator.NuGet;
using Cake.MetadataGenerator.Settings;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.NuGet
{
    public partial class NugetPackageManagerProviderTests
    {
        public class GetMethod : Test<NugetPackageManagerProvider>
        {
            [Fact]
            public void ReturnsPackageManagerWithProperLocalRepositoryPath()
            {
                var packageRepository = Use<IPackageRepository>();
                var localRepositoryPath = @"C:\Temp";
                Get<INuGetSettings>().LocalRepositoryPath.Returns(localRepositoryPath);
                Get<INugetPackageRepositoryProvider>().Get().Returns(packageRepository);

                var packageManager = Subject.Get();

                packageManager.SourceRepository.Should().BeSameAs(packageRepository);
                var repositoryPath = Get<INuGetSettings>().Received(1).LocalRepositoryPath;
            }
        }
    }
}