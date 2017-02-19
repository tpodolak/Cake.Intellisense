using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public interface IPackageManagerProvider
    {
        global::NuGet.IPackageManager Get();
    }
}