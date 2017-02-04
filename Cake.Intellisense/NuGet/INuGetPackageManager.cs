using System.Collections.Generic;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public interface INuGetPackageManager
    {
        IPackage InstallPackage(string packageId, string version);

        List<FrameworkName> GetTargetFrameworks(IPackage package);
    }
}