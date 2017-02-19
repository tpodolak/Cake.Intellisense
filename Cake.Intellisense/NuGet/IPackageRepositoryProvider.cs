using NuGet;

namespace Cake.MetadataGenerator.NuGet
{
    public interface IPackageRepositoryProvider
    {
        IPackageRepository Get();
    }
}