#load "./imports.csx"
#load "./parameters.csx"
#load "./version.csx"
#load "./paths.csx"
// Install tools.
#tool "nuget:https://www.nuget.org/api/v2?package=gitreleasemanager&version=0.5.0"
#tool "nuget:https://www.nuget.org/api/v2?package=GitVersion.CommandLine&version=3.6.2"
#tool "nuget:https://www.nuget.org/api/v2?package=coveralls.io&version=1.3.4"
#tool "nuget:https://www.nuget.org/api/v2?package=OpenCover&version=4.6.519"
#tool "nuget:https://www.nuget.org/api/v2?package=ReportGenerator&version=2.4.5"
#tool "nuget:https://www.nuget.org/api/v2?package=xunit.runner.console&version=2.1.0"

using Cake.Common.Tools.NuGet.Pack;

var parameters = BuildParameters.GetParameters(Context);
var buildVersion = BuildVersion.Calculate(Context);
var paths = BuildPaths.GetPaths(Context, parameters, buildVersion);

Setup(context =>
{
    Information("Building version {0} of Cake.Intellisense", buildVersion.SemVersion);
    if(!DirectoryExists(paths.Directories.Artifacts))
    {
        CreateDirectory(paths.Directories.Artifacts);
    }
});

Task("Clean")
    .Does(()=>
{
    CleanDirectories(paths.Directories.ToClean);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(paths.Files.Solution);
});

Task("Run-Tests")
    .IsDependentOn("Build")
    .Does(()=>
{
    XUnit2(paths.Files.TestAssemblies, new XUnit2Settings 
    { 
        Parallelism = ParallelismOption.All
    });
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() => 
{
    var setings = new MSBuildSettings
                        {
                            Configuration = parameters.Configuration
                        };
    MSBuild(paths.Files.Solution, setings);
});

Task("Pack")
.WithCriteria(val => parameters.Configuration == "Release")
.IsDependentOn("Run-Tests")
.Does(() =>
{
    NuGetPack(paths.Files.CakeIntellisenseNuSpec, new NuGetPackSettings 
    {
        Version = buildVersion.SemVersion,
        OutputDirectory = paths.Directories.Artifacts
    });
});

RunTarget(parameters.Target);