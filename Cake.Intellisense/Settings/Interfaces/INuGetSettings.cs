namespace Cake.Intellisense.Settings.Interfaces
{
    public interface INuGetSettings : ISettings
    {
        string PackageSource { get; set; }

        string LocalRepositoryPath { get; set; }

        bool AllowPreReleaseVersions { get; set; }
    }
}