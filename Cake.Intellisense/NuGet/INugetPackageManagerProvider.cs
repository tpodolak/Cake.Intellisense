using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public interface INugetPackageManagerProvider
    {
        IPackageManager Get();
    }
}