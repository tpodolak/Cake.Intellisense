using System;
using System.IO;
using Cake.Intellisense.NuGet.Interfaces;
using NuGet;

namespace Cake.Intellisense.NuGet
{
    public class PackageAssemblyReferencePathResolver : IPackageAssemblyReferencePathResolver
    {
        private readonly IPackagePathResolver _pathResolver;

        public PackageAssemblyReferencePathResolver(IPackagePathResolver pathResolver)
        {
            _pathResolver = pathResolver ?? throw new ArgumentNullException(nameof(pathResolver));
        }

        public string GetPath(IPackage package, IPackageAssemblyReference packageAssemblyReference)
        {
            return Path.Combine(_pathResolver.GetInstallPath(package), packageAssemblyReference.Path);
        }
    }
}