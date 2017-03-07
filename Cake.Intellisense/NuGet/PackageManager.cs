using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Cake.Intellisense.Settings;
using NuGet;

namespace Cake.Intellisense.NuGet
{
    public class PackageManager : IPackageManager
    {
        private readonly global::NuGet.IPackageManager packageManager;
        private readonly IPackageRepository packageRepository;
        private readonly INuGetSettings settings;

        public PackageManager(
            IPackageManagerProvider packageManagerProvider,
            IPackageRepositoryProvider packageRepositoryProvider,
            INuGetSettings settings)
        {
            this.settings = settings;
            packageManager = packageManagerProvider.Get();
            packageRepository = packageRepositoryProvider.Get();
        }

        public IPackage InstallPackage(string packageId, string version, FrameworkName targetFramework)
        {
            var package = FindPackage(packageId, version, targetFramework);

            if (package == null)
                return null;

            packageManager.InstallPackage(package, false, true);

            return package;
        }

        public List<FrameworkName> GetTargetFrameworks(IPackage package)
        {
            return
                package.GetFiles()
                    .GroupBy(val => val.TargetFramework, packageFile => packageFile.TargetFramework)
                    .Select(group => group.Key)
                    .ToList();
        }

        public IPackage FindPackage(string packageId, string version, FrameworkName targetFramework)
        {
            var package = FindPackage(packageId, version);

            if (package != null && GetTargetFrameworks(package).Any(framework => framework == targetFramework))
                return package;

            return null;
        }

        public IPackage FindPackage(string packageId, string version)
        {
            var packages = packageRepository.FindPackagesById(packageId).ToList();

            if (string.IsNullOrWhiteSpace(version))
                return packages.LastOrDefault(package => settings.AllowPreReleaseVersions || package.IsReleaseVersion());

            var semanticVersion = new SemanticVersion(new Version(version));

            return packageRepository.FindPackagesById(packageId).SingleOrDefault(package => package.Version == semanticVersion);
        }
    }
}