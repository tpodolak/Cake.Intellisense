using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.Intellisense.NuGet.Interfaces;
using Cake.Intellisense.Reflection.Interfaces;
using NuGet;

namespace Cake.Intellisense.NuGet
{
    public class PackageAssemblyResolver : IPackageAssemblyResolver
    {
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly IPackageAssemblyReferencePathResolver _pathResolver;

        public PackageAssemblyResolver(IAssemblyLoader assemblyLoader, IPackageAssemblyReferencePathResolver pathResolver)
        {
            _assemblyLoader = assemblyLoader;
            _pathResolver = pathResolver;
        }

        public List<Assembly> ResolveAssemblies(IPackage package, FrameworkName targetFramework)
        {
            return package.AssemblyReferences
                  .Where(val => val.SupportedFrameworks.Contains(targetFramework))
                  .Select(val => _assemblyLoader.LoadFrom(_pathResolver.GetPath(package, val)))
                  .ToList();
        }
    }
}