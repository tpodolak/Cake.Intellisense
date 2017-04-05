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

#addin Cake.Coveralls

var parameters = BuildParameters.GetParameters(Context);
var buildVersion = BuildVersion.Calculate(Context);
var paths = BuildPaths.GetPaths(Context, parameters, buildVersion);
var publishingError = false;
var packages = BuildPackages.GetPackages(paths, buildVersion);

Setup(context =>
{
    Information("Building version {0} of Cake.Intellisense", buildVersion.SemVersion);
    if (!DirectoryExists(paths.Directories.Artifacts))
    {
        CreateDirectory(paths.Directories.Artifacts);
    }

    if (!DirectoryExists(paths.Directories.TestResults))
    {
        CreateDirectory(paths.Directories.TestResults);
    }

    if (!FileExists(paths.Files.ReleaseNotes))
    {
        using (System.IO.File.Create(paths.Files.ReleaseNotes.ToString())) ;
    }
});

Task("Clean")
    .Does(() =>
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
    .Does(() =>
{
    Action<ICakeContext> testAction = context =>
    {
        context.XUnit2(paths.Files.TestAssemblies, new XUnit2Settings
        {
            Parallelism = ParallelismOption.All,
            ShadowCopy = false,
        });
    };

    if (parameters.SkipOpenCover)
    {
        testAction(Context);
    }
    else
    {
        OpenCover(testAction,
                        paths.Files.TestCoverageOutputFilePath,
                        new OpenCoverSettings
                        {
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
    MSBuild(paths.Files.Solution, settings => settings.SetConfiguration(parameters.Configuration));
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


Task("Zip-Files")
.IsDependentOn("Run-Tests")
    .Does(() =>
{
    var rootPath = paths.Directories.RootDir.Combine("Cake.Intellisense").Combine("bin").Combine(parameters.Configuration);
    var files = GetFiles(rootPath + "/**/*");
    Zip(rootPath, packages.ZipPackage, files);
});


Task("Publish-GitHub-Release")
    .WithCriteria(() => parameters.ShouldPublish)
    .IsDependentOn("Zip-Files")
    .Does(() =>
{
    var userName = EnvironmentVariable("GITHUB_USERNAME");
    var password = EnvironmentVariable("GITHUB_PASSWORD");

    if (string.IsNullOrWhiteSpace(userName))
    {
        throw new InvalidOperationException("Could not resolve Github username.");
    }

    if (string.IsNullOrWhiteSpace(password))
    {
        throw new InvalidOperationException("Could not resolve Github password.");
    }

    Version parsedVersion;
    GitReleaseManagerCreate(userName, password, "tpodolak", "Cake.Intellisense", new GitReleaseManagerCreateSettings
    {
        Name = buildVersion.SemVersion,
        Prerelease = !Version.TryParse(buildVersion.SemVersion, out parsedVersion),
        TargetCommitish = "master",
        InputFilePath = MakeAbsolute(paths.Files.ReleaseNotes)
    });

    GitReleaseManagerAddAssets(userName, password, "tpodolak", "Cake.Intellisense", buildVersion.SemVersion, MakeAbsolute(packages.ZipPackage).ToString());
    GitReleaseManagerClose(userName, password, "tpodolak", "Cake.Intellisense", buildVersion.SemVersion);
}).OnError(exception =>
{
    Information("Publish-GitHub-Release Task failed, but continuing with next Task...");
    publishingError = true;
});

Task("Publish")
.WithCriteria(context => parameters.ShouldPublish)
.IsDependentOn("Publish-NuGet")
.IsDependentOn("Publish-GitHub-Release");

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
}).OnError(exception =>
{
    Information("Publish-NuGet Task failed, but continuing with next Task...");
    publishingError = true;
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


Task("Upload-Coverage-Report")
    .WithCriteria(() => FileExists(paths.Files.TestCoverageOutputFilePath))
    .WithCriteria(() => !parameters.IsLocalBuild)
    .WithCriteria(() => !parameters.IsPullRequest)
    .WithCriteria(() => parameters.IsMaster)
    .IsDependentOn("Publish-NuGet")
    .Does(() =>
{
    var repoKey = EnvironmentVariable("COVERALLS_REPO_TOKEN");
    if (string.IsNullOrEmpty(repoKey))
    {
        throw new InvalidOperationException("Could not resolve Coveralls Repo key.");
    }

    CoverallsIo(paths.Files.TestCoverageOutputFilePath, new CoverallsIoSettings
    {
        RepoToken = repoKey
    });
});

Task("AppVeyor")
  .IsDependentOn("Upload-Coverage-Report")
  .IsDependentOn("Publish")
  .Finally(() =>
{
    if (publishingError)
    {
        throw new Exception("An error occurred during the publishing of Cake.  All publishing tasks have been attempted.");
    }
});

RunTarget(parameters.Target);