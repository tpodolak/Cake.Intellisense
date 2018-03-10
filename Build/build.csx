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
#tool "nuget:https://www.nuget.org/api/v2?package=JetBrains.ReSharper.CommandLineTools&version=2017.1.20170407.131846"
#tool "nuget:https://www.nuget.org/api/v2?package=xunit.runner.visualstudio&version=2.2.0"
#addin "nuget:https://www.nuget.org/api/v2?package=cake.coveralls&version=0.4.0"

var parameters = BuildParameters.GetParameters(Context);
var buildVersion = BuildVersion.Calculate(Context);
var paths = BuildPaths.GetPaths(Context, parameters, buildVersion);
var publishingError = false;
var packages = BuildPackages.GetPackages(paths, buildVersion);
var releaseNotes = ParseReleaseNotes(paths.Files.AllReleaseNotes);

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

    if (FileExists(paths.Files.CurrentReleaseNotes))
    {
        DeleteFile(paths.Files.CurrentReleaseNotes);
    }
});

Task("Clean")
    .Does(() =>
{
    CleanDirectories(paths.Directories.ToClean);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .IsDependentOn("Patch-AssemblyInfo")
    .Does(() =>
{
    DotNetCoreRestore(paths.Files.Solution.ToString());
});

Task("Run-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    Action<ICakeContext> testAction = context =>
    {
        var testAdapterPath = context.Directory("tools/xunit.runner.visualstudio/build/_common");
        var argumentBuilder = new ProcessArgumentBuilder();
        argumentBuilder.Append("vstest")
                    .Append(string.Join(" ", paths.Files.TestAssemblies.Select(val => MakeAbsolute(val).ToString())))
                    .AppendSwitch("--Framework:", string.Empty, parameters.TargetFrameworkFull)
                    .AppendSwitch("--TestAdapterPath:", string.Empty, MakeAbsolute(testAdapterPath).ToString())
                    .Append("--Parallel");

        context.StartProcess("dotnet.exe", new ProcessSettings
        {
            Arguments = argumentBuilder
        });
    };

    if (parameters.SkipOpenCover)
    {
        testAction(Context);
    }
    else
    {
        OpenCover(testAction,
                        paths.Files.TestCoverageOutput,
                        new OpenCoverSettings
                        {
                            ReturnTargetCodeOffset = 0,
                            OldStyle = true,
                            ArgumentCustomization = args => args.Append("-mergeoutput")

                        }
                        .WithFilter("+[Cake.Intellisense*]* -[Cake.Intellisense.Tests*]*")
                        .ExcludeByAttribute("*.ExcludeFromCodeCoverage*")
                        .ExcludeByFile("*/*Designer.cs;*/*.g.cs;*/*.g.i.cs"));
        ReportGenerator(paths.Files.TestCoverageOutput, paths.Directories.TestResults);
    }

});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Find-Duplicates")
    .Does(() =>
{
    DotNetCoreBuild(paths.Files.Solution.ToString(), new DotNetCoreBuildSettings
    {
        Configuration = parameters.Configuration,
    });
});

Task("NuGet-Pack")
.IsDependentOn("Run-Tests")
.WithCriteria(val => parameters.Configuration == "Release")
.Does(() =>
{
    NuGetPack(paths.Files.CakeIntellisenseNuSpec, new NuGetPackSettings
    {
        Version = buildVersion.SemVersion,
        OutputDirectory = paths.Directories.Artifacts,
        ReleaseNotes = releaseNotes.Notes.ToArray()
    });
});


Task("Zip-Files")
.IsDependentOn("Run-Tests")
    .Does(() =>
{
    var rootPath = paths.Directories.RootDir.Combine("Cake.Intellisense")
                                            .Combine("bin")
                                            .Combine(parameters.Configuration)
                                            .Combine(parameters.TargetFramework);
    var files = GetFiles(rootPath + "/**/*");
    Zip(rootPath, packages.ZipPackage, files);
});


Task("Publish-GitHub-Release")
    .WithCriteria(context => parameters.ShouldPublish && releaseNotes.Version.ToString() == buildVersion.SemVersion)
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
    System.IO.File.WriteAllLines(MakeAbsolute(paths.Files.CurrentReleaseNotes).ToString(), releaseNotes.Notes);
    GitReleaseManagerCreate(userName, password, "tpodolak", "Cake.Intellisense", new GitReleaseManagerCreateSettings
    {
        Name = buildVersion.SemVersion,
        Prerelease = !Version.TryParse(buildVersion.SemVersion, out parsedVersion),
        TargetCommitish = "master",
        InputFilePath = MakeAbsolute(paths.Files.CurrentReleaseNotes)
    });

    GitReleaseManagerAddAssets(userName, password, "tpodolak", "Cake.Intellisense", buildVersion.SemVersion, MakeAbsolute(packages.ZipPackage).ToString());
    GitReleaseManagerClose(userName, password, "tpodolak", "Cake.Intellisense", buildVersion.SemVersion);
    GitReleaseManagerPublish(userName, password, "tpodolak", "Cake.Intellisense", buildVersion.SemVersion);

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
    .IsDependentOn("NuGet-Pack")
    .WithCriteria(context => parameters.ShouldPublish && releaseNotes.Version.ToString() == buildVersion.SemVersion)
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
.WithCriteria(() => !parameters.IsLocalBuild)
.Does(() =>
{
    GitVersion(new GitVersionSettings
    {
        UpdateAssemblyInfo = true,
        WorkingDirectory = paths.Directories.RootDir
    });
});


Task("Upload-Coverage-Report")
    .WithCriteria(() => FileExists(paths.Files.TestCoverageOutput))
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

    CoverallsIo(paths.Files.TestCoverageOutput, new CoverallsIoSettings
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


Task("Find-Duplicates")
.Does(() =>
{
    var projectFile = paths.Directories.RootDir.Combine("Cake.Intellisense")
                                               .CombineWithFilePath("Cake.Intellisense.csproj");

    DupFinder(projectFile, new DupFinderSettings
    {
        ShowText = true,
        OutputFile = paths.Files.DupeFinderOutput,
        ThrowExceptionOnFindingDuplicates = true
    });
});

Teardown(context =>
{
    var result = context.Successful ? "succeeded" : "failed";
    Information("Cake.Intellisense {0} - build {1}", buildVersion.SemVersion, result);
});

RunTarget(parameters.Target);