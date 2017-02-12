using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public class NuGetDependencyResolver : INuGetDependencyResolver
    {
        private readonly IPackageRepository packageRepository;

        public NuGetDependencyResolver(INugetPackageRepositoryProvider packageRepositoryProvider)
        {
            packageRepository = packageRepositoryProvider.Get();
        }

        public List<IPackage> GetDependentPackagesAndSelf(IPackage package, FrameworkName frameworkName)
        {
            if (package == null)
                return new List<IPackage>();

            var packages = new List<IPackage> { package };

            var selectMany = package.DependencySets.SelectMany(x => x.Dependencies)
                .Select(dep => new
                {
                    dependency = dep,
                    dependentPackage = packageRepository.ResolveDependency(dep, false, true)
                })
                .Where(val => val.dependentPackage.GetFiles().Any(file => file.Path.EndsWith(".dll") && file.SupportedFrameworks.Contains(frameworkName)))
                .SelectMany(dep => GetDependentPackagesAndSelf(dep.dependentPackage, frameworkName));

            return packages.Union(selectMany).ToList();
        }
    }
}