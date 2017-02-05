using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.Settings;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public class NuGetPackageManager : INuGetPackageManager
    {
        private readonly IPackageManager packageManager;
        private readonly IPackageRepository packageRepository;
        private readonly INuGetSettings settings;

        public NuGetPackageManager(INugetPackageManagerProvider packageManagerProvider,
            INugetPackageRepositoryProvider packageRepositoryProvider,
            INuGetSettings settings)
        {
            this.settings = settings;
            this.packageManager = packageManagerProvider.Get();
            this.packageRepository = packageRepositoryProvider.Get();
        }

        public IPackage InstallPackage(string packageId, string version, FrameworkName targetFramework)
        {
            var package = FindPackage(packageId, version, targetFramework);

            if (package == null)
                return null;

            this.packageManager.InstallPackage(package, false, true);

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
            var packages = packageRepository.FindPackagesById(packageId);

            if (string.IsNullOrWhiteSpace(version))
                return packages.LastOrDefault(package => settings.AllowPreReleaseVersions || package.IsReleaseVersion());

            var semanticVersion = new SemanticVersion(new Version(version));

            return packageRepository.FindPackagesById(packageId).SingleOrDefault(package => package.Version == semanticVersion);
        }
    }
}