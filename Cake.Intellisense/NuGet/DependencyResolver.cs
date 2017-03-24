using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.Intellisense.NuGet
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IPackageRepository packageRepository;

        public DependencyResolver(IPackageRepository packageRepositoryProvider)
        {
            packageRepository = packageRepositoryProvider;
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
            return val.AssemblyReferences.Any(assemblyReference => assemblyReference.Path.EndsWith(".dll") && assemblyReference.SupportedFrameworks.Contains(frameworkName));
        }
    }
}