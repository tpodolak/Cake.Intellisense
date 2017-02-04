using Cake.MetadataGenerator.Settings;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public class NugetPackageManagerProvider : INugetPackageManagerProvider
    {
        private readonly INugetPackageRepositoryProvider nugetPackageRepositoryProvider;
        private readonly INuGetSettings settings;

        public NugetPackageManagerProvider(INugetPackageRepositoryProvider nugetPackageRepositoryProvider, INuGetSettings settings)
        {
            this.nugetPackageRepositoryProvider = nugetPackageRepositoryProvider;
            this.settings = settings;
        }

        public IPackageManager Get()
        {
            var packageRepository = nugetPackageRepositoryProvider.Get();
            return new PackageManager(packageRepository, settings.LocalRepositoryPath);
        }
    }
}