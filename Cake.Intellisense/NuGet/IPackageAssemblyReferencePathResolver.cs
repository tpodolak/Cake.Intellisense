using NuGet;

namespace Cake.Intellisense.NuGet
{
    public interface IPackageAssemblyReferencePathResolver
    {
        string GetPath(IPackage package, IPackageAssemblyReference packageAssemblyReference);
    }
}