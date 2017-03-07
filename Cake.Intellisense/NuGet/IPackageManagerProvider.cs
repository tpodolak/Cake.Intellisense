namespace Cake.Intellisense.NuGet
{
    public interface IPackageManagerProvider
    {
        global::NuGet.IPackageManager Get();
    }
}