using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Cake.Intellisense.Settings.Interfaces;
using NuGet;
using IPackageManager = Cake.Intellisense.NuGet.Interfaces.IPackageManager;

namespace Cake.Intellisense.NuGet
{
    public class PackageManager : IPackageManager
    {
        private readonly global::NuGet.IPackageManager _packageManager;
        private readonly IPackageRepository _packageRepository;
        private readonly INuGetSettings _settings;

        public PackageManager(
            IPackageRepository packageRepository,
            global::NuGet.IPackageManager packageManager,
            INuGetSettings settings)
        {
            _packageRepository = packageRepository ?? throw new ArgumentNullException(nameof(packageRepository));
            _packageManager = packageManager ?? throw new ArgumentNullException(nameof(packageManager));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public IPackage InstallPackage(string packageId, string version, FrameworkName targetFramework)
        {
            var package = FindPackage(packageId, version, targetFramework);

            if (package == null)
                return null;

            _packageManager.InstallPackage(package, false, true);

            return package;
        }

        public List<FrameworkName> GetTargetFrameworks(IPackage package)
        {
            return
                package.AssemblyReferences
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
            var packages = _packageRepository.FindPackagesById(packageId).ToList();

            if (string.IsNullOrWhiteSpace(version))
                return packages.LastOrDefault(package => _settings.AllowPreReleaseVersions || package.IsReleaseVersion());

            var semanticVersion = new SemanticVersion(new Version(version));

            return _packageRepository.FindPackagesById(packageId).SingleOrDefault(package => package.Version == semanticVersion);
        }
    }
}