using System.Collections.Generic;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public interface INuGetDependencyResolver
    {
        List<IPackage> GetDependentPackagesAndSelf(IPackage package, FrameworkName frameworkName);
    }
}