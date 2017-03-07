using NuGet;

namespace Cake.Intellisense.NuGet
{
    public interface IPackageRepositoryProvider
    {
        IPackageRepository Get();
    }
}