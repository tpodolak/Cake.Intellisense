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
using Cake.Common.Tools.NuGet.Push;
using Cake.Core.Diagnostics;

var parameters = BuildParameters.GetParameters(Context);
var buildVersion = BuildVersion.Calculate(Context);
var paths = BuildPaths.GetPaths(Context, parameters, buildVersion);

var packages = BuildPackages.GetPackages(paths, buildVersion);

Setup(context =>
{
    Information("Building version {0} of Cake.Intellisense", buildVersion.SemVersion);
    if(!DirectoryExists(paths.Directories.Artifacts))
    {
        CreateDirectory(paths.Directories.Artifacts);
    }

    if (!DirectoryExists(paths.Directories.TestResults))
    {
        CreateDirectory(paths.Directories.TestResults);
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
    Action<ICakeContext> testAction = context => 
    {
        context.XUnit2(paths.Files.TestAssemblies, new XUnit2Settings 
        { 
            Parallelism = ParallelismOption.All,
            ShadowCopy = false,
        });
    };

    if(parameters.SkipOpenCover)
    {
        testAction(Context);
    }
    else
    {
        OpenCover(testAction,
                        paths.Files.TestCoverageOutputFilePath,
                        new OpenCoverSettings {
                            ReturnTargetCodeOffset = 0,
                            ArgumentCustomization = args => args.Append("-mergeoutput")
                        }
                        .WithFilter("+[Cake.Intellisense*]* -[Cake.Intellisense.Tests*]*")
                        .ExcludeByAttribute("*.ExcludeFromCodeCoverage*")
                        .ExcludeByFile("*/*Designer.cs;*/*.g.cs;*/*.g.i.cs"));

        ReportGenerator(paths.Files.TestCoverageOutputFilePath, paths.Directories.TestResults);
    }

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
.IsDependentOn("Patch-AssemblyInfo")
.IsDependentOn("Run-Tests")
.WithCriteria(val => parameters.Configuration == "Release")
.Does(() =>
{
    NuGetPack(paths.Files.CakeIntellisenseNuSpec, new NuGetPackSettings 
    {
        Version = buildVersion.SemVersion,
        OutputDirectory = paths.Directories.Artifacts
    });
});

Task("Publish-NuGet")
    .IsDependentOn("Pack")
    .WithCriteria(context => parameters.ShouldPublish)
    .Does(() =>
{
    var apiKey = EnvironmentVariable("NUGET_API_KEY");
    if (string.IsNullOrEmpty(apiKey))
    {
        throw new InvalidOperationException("Could not resolve NuGet API key.");
    }

    var apiUrl = EnvironmentVariable("NUGET_API_URL");
    if (string.IsNullOrEmpty(apiUrl))
    {
        throw new InvalidOperationException("Could not resolve NuGet API url.");
    }

    NuGetPush(packages.NuGetPackage, new NuGetPushSettings
    {
        ApiKey = apiKey,
        Source = apiUrl
    });
});

Task("Patch-AssemblyInfo")
.Does(() => 
{
    GitVersion(new GitVersionSettings
    {
        UpdateAssemblyInfo = true,
        WorkingDirectory = paths.Directories.RootDir
    });
});

Task("AppVeyor")
  .IsDependentOn("Publish-NuGet");

RunTarget(parameters.Target);