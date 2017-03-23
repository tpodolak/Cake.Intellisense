using System.IO;
using NuGet;

namespace Cake.Intellisense.NuGet
{
    public class PackageAssemblyReferencePathResolver : IPackageAssemblyReferencePathResolver
    {
        private readonly IPackagePathResolver pathResolver;

        public PackageAssemblyReferencePathResolver(IPackagePathResolver pathResolver)
        {
            this.pathResolver = pathResolver;
        }

        public string GetPath(IPackage package, IPackageAssemblyReference packageAssemblyReference)
        {
            return Path.Combine(pathResolver.GetInstallPath(package), packageAssemblyReference.Path);
        }
    }
}