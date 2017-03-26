using NuGet;

namespace Cake.Intellisense.NuGet.Interfaces
{
    public interface IPackageAssemblyReferencePathResolver
    {
        string GetPath(IPackage package, IPackageAssemblyReference packageAssemblyReference);
    }
}