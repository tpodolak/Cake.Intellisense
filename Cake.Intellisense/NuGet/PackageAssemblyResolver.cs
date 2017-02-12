using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.Reflection;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public class PackageAssemblyResolver : IPackageAssemblyResolver
    {
        private readonly IAssemblyLoader assemblyLoader;

        public PackageAssemblyResolver(IAssemblyLoader assemblyLoader)
        {
            this.assemblyLoader = assemblyLoader;
        }

        public List<Assembly> ResolveAssemblies(IPackage package, FrameworkName targetFramework)
        {
            // TODO how can we get rid of cast, loading assembly from stream drops the location
            return package.GetFiles().OfType<PhysicalPackageFile>()
                  .Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll"))
                  .Select(val => assemblyLoader.LoadFrom(val.SourcePath))
                  .ToList();
        }
    }
}