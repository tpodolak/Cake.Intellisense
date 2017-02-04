using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public interface INugetPackageRepositoryProvider
    {
        IPackageRepository Get();
    }
}