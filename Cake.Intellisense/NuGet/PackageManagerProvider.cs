using Cake.Intellisense.Logging;
using Cake.Intellisense.Settings;
using NLog;

namespace Cake.Intellisense.NuGet
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
            var packageManager = new global::NuGet.PackageManager(packageRepository, settings.LocalRepositoryPath)
            {
                Logger = new NLogNugetLoggerAdapter(LogManager.GetCurrentClassLogger(typeof(global::NuGet.PackageManager)))
            };

            return packageManager;
        }
    }
}