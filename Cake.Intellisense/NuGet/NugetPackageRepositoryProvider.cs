using Cake.MetadataGenerator.Logging;
using Cake.MetadataGenerator.Settings;
using NLog;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public class NugetPackageRepositoryProvider : INugetPackageRepositoryProvider
    {
        private readonly IPackageRepositoryFactory packageRepositoryFactory;
        private readonly INuGetSettings settings;

        public NugetPackageRepositoryProvider(IPackageRepositoryFactory packageRepositoryFactory, INuGetSettings settings)
        {
            this.packageRepositoryFactory = packageRepositoryFactory;
            this.settings = settings;
        }

        public IPackageRepository Get()
        {
            var packageRepository = packageRepositoryFactory.CreateRepository(settings.PackageSource);
            InitializeLogger(packageRepository);
            return packageRepository;
        }

        private void InitializeLogger(IPackageRepository packageRepository)
        {
            var baseRepository = packageRepository as PackageRepositoryBase;
            if (baseRepository != null)
            {
                baseRepository.Logger = new NLogNugetLoggerAdapter(LogManager.GetLogger(baseRepository.GetType().FullName));
            }
        }
    }
}