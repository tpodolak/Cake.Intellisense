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

            return
                new List<IPackage> { package }.Union(package.DependencySets.SelectMany(x => x.Dependencies)
                        .Where(
                            dep =>
                                packageRepository.ResolveDependency(dep, false, true)
                                    .GetFiles().Where(file => file.Path.EndsWith(".dll"))
                                    .Any(file => file.SupportedFrameworks.Contains(frameworkName)))
                        .SelectMany(
                            x =>
                                GetDependentPackagesAndSelf(packageRepository.ResolveDependency(x, false, true),
                                    frameworkName)))
                    .ToList();
        }
    }
}