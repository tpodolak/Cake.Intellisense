using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.Intellisense.NuGet
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IPackageRepository packageRepository;

        public DependencyResolver(IPackageRepositoryProvider packageRepositoryProvider)
        {
            packageRepository = packageRepositoryProvider.Get();
        }

        public IEnumerable<IPackage> GetDependentPackagesAndSelf(IPackage package, FrameworkName frameworkName)
        {
            if (package == null)
                yield break;

            yield return package;

            foreach (var dependency in GetDependentPackages(package, frameworkName))
            {
                foreach (var innerPackage in GetDependentPackagesAndSelf(dependency, frameworkName))
                {
                    yield return innerPackage;
                }
            }
        }

        private IEnumerable<IPackage> GetDependentPackages(IPackage package, FrameworkName frameworkName)
        {
            return package.DependencySets.SelectMany(x => x.Dependencies)
                .Select(val => packageRepository.ResolveDependency(val, false, true))
                .Where(val => IsValidDependency(val, frameworkName));
        }

        private bool IsValidDependency(IPackage val, FrameworkName frameworkName)
        {
            return val.GetFiles().Any(file => file.Path.EndsWith(".dll") && file.SupportedFrameworks.Contains(frameworkName));
        }
    }
}