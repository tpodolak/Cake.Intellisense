using System.Collections.Generic;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public interface INuGetDependencyResolver
    {
        IEnumerable<IPackage> GetDependentPackagesAndSelf(IPackage package, FrameworkName frameworkName);
    }
}