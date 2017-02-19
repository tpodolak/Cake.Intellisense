using System.Collections.Generic;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public interface IDependencyResolver
    {
        IEnumerable<IPackage> GetDependentPackagesAndSelf(IPackage package, FrameworkName frameworkName);
    }
}