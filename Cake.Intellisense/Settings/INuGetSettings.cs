namespace Cake.MetadataGenerator.Settings
{
    public interface INuGetSettings : ISettings
    {
        string PackageSource { get; set; }

        string LocalRepositoryPath { get; set; }

        bool AllowPreReleaseVersions { get; set; }
    }
}