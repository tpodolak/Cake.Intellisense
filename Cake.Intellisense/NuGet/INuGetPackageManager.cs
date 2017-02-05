﻿using System.Collections.Generic;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public interface INuGetPackageManager
    {
        IPackage InstallPackage(string packageId, string version, FrameworkName targetFramework);

        List<FrameworkName> GetTargetFrameworks(IPackage package);

        IPackage FindPackage(string packageId, string version);

        IPackage FindPackage(string packageId, string version, FrameworkName targetFramework);
    }
}