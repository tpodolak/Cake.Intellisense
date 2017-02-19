using Cake.MetadataGenerator.Settings;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public class PackageManagerProvider : IPackageManagerProvider
    {
        private readonly IPackageRepositoryProvider packageRepositoryProvider;
        private readonly INuGetSettings settings;

        public PackageManagerProvider(IPackageRepositoryProvider packageRepositoryProvider, INuGetSettings settings)
        {
            this.packageRepositoryProvider = packageRepositoryProvider;
            this.settings = settings;
        }

        public global::NuGet.IPackageManager Get()
        {
            var packageRepository = packageRepositoryProvider.Get();
            return new global::NuGet.PackageManager(packageRepository, settings.LocalRepositoryPath);
        }
    }
}