using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.Intellisense.Reflection;
using NuGet;

namespace Cake.Intellisense.NuGet
{
    public class PackageAssemblyResolver : IPackageAssemblyResolver
    {
        private readonly IAssemblyLoader assemblyLoader;
        private readonly IPackageAssemblyReferencePathResolver pathResolver;

        public PackageAssemblyResolver(IAssemblyLoader assemblyLoader, IPackageAssemblyReferencePathResolver pathResolver)
        {
            this.assemblyLoader = assemblyLoader;
            this.pathResolver = pathResolver;
        }

        public List<Assembly> ResolveAssemblies(IPackage package, FrameworkName targetFramework)
        {
            return package.AssemblyReferences
                  .Where(val => val.SupportedFrameworks.Contains(targetFramework))
                  .Select(val => assemblyLoader.LoadFrom(pathResolver.GetPath(package, val)))
                  .ToList();
        }
    }
}