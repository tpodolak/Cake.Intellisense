using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Cake.Intellisense.Settings.Interfaces;
using NuGet;
using IDependencyResolver = Cake.Intellisense.NuGet.Interfaces.IDependencyResolver;

namespace Cake.Intellisense.NuGet
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IPackageRepository _packageRepository;
        private readonly INuGetSettings _nugetSettings;

        public DependencyResolver(IPackageRepository packageRepository, INuGetSettings nugetSettings)
        {
            _packageRepository = packageRepository ?? throw new ArgumentNullException(nameof(packageRepository));
            _nugetSettings = nugetSettings ?? throw new ArgumentNullException(nameof(nugetSettings));
        }

        public IEnumerable<IPackage> GetDependenciesAndSelf(IPackage package, FrameworkName frameworkName)
        {
            yield return package;

            foreach (var dependency in GetDependencies(package, frameworkName))
            {
                if (_nugetSettings.RecursiveDependencyResolution)
                {
                    foreach (var innerPackage in GetDependenciesAndSelf(dependency, frameworkName))
                        yield return innerPackage;
                }
                else
                {
                    yield return dependency;
                }
            }
        }

        private IEnumerable<IPackage> GetDependencies(IPackage package, FrameworkName frameworkName)
        {
            return package.DependencySets.SelectMany(x => x.Dependencies)
                          .Select(val => _packageRepository.ResolveDependency(val, false, true))
                          .Where(val => IsValidDependency(val, frameworkName));
        }

        private bool IsValidDependency(IPackage val, FrameworkName frameworkName)
        {
            return val.AssemblyReferences.Any(assemblyReference => assemblyReference.Path.EndsWith(".dll") && assemblyReference.SupportedFrameworks.Contains(frameworkName));
        }
    }
}