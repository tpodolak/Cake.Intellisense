using System.Collections.Generic;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.Intellisense.NuGet.Interfaces
{
    public interface IPackageManager
    {
        IPackage InstallPackage(string packageId, string version, FrameworkName targetFramework);

        List<FrameworkName> GetTargetFrameworks(IPackage package);

        IPackage FindPackage(string packageId, string version);

        IPackage FindPackage(string packageId, string version, FrameworkName targetFramework);
    }
}