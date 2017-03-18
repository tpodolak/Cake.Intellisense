#load "./imports.csx"
#load "./parameters.csx"

using System.Collections.Generic;
using System.Linq;

public class BuildPaths
{
    public BuildFiles Files { get; private set; }
    public BuildDirectories Directories { get; private set; }

    public static BuildPaths GetPaths(ICakeContext context, BuildParameters parameters, BuildVersion version)
    {
        var configuration =  parameters.Configuration;
        var buildDirectories = GetBuildDirectories(context, version);
        var testAssemblies = buildDirectories.TestDirs
                                             .Select(dir => dir.Combine("bin").Combine(configuration).CombineWithFilePath(dir.GetDirectoryName() + ".dll"))
                                             .ToList();

        var buildFiles = new BuildFiles(
            buildDirectories.RootDir.CombineWithFilePath("Cake.Intellisense.sln"),
            buildDirectories.RootDir.CombineWithFilePath("Cake.Inellisense.nuspec"),
            buildDirectories.TestResults.CombineWithFilePath("OpenCover.xml"),
            testAssemblies);
        
        return new BuildPaths
        {
            Files = buildFiles,
            Directories = buildDirectories
        };
    }

    public static BuildDirectories GetBuildDirectories(ICakeContext context, BuildVersion version)
    {
        var rootDir = (DirectoryPath)context.Directory("../");
        var artifacts = rootDir.Combine(".artifacts");
        var testResults = artifacts.Combine("Test-Results");
        var integrationTestsDir = rootDir.Combine(context.Directory("Cake.Intellisense.Tests.Integration"));
        var unitTestsDir = rootDir.Combine(context.Directory("Cake.Intellisense.Tests.Unit"));
        var mainProjectDir = rootDir.Combine(context.Directory("Cake.Intellisense"));

        var testDirs = new []{
                                unitTestsDir,
                                integrationTestsDir
                            };
        var toClean = new[] {
                                 testResults,
                                 integrationTestsDir.Combine("bin"),
                                 integrationTestsDir.Combine("obj"),
                                 unitTestsDir.Combine("bin"),
                                 unitTestsDir.Combine("obj"),
                                 mainProjectDir.Combine("bin"),
                                 mainProjectDir.Combine("obj"),
                            };
        return new BuildDirectories(rootDir,
                                    artifacts,
                                    testResults,
                                    testDirs, 
                                    toClean);
    }
}

public class BuildFiles
{
    public FilePath Solution { get; private set; }
    public FilePath TestCoverageOutputFilePath { get; set;}
    public FilePath CakeIntellisenseNuSpec { get; private set; }
    public ICollection<FilePath> TestAssemblies { get; private set; }
    public BuildFiles(FilePath solution,
                      FilePath cakeIntellisenseNuSpec,
                      FilePath testCoverageOutputFilePath,
                      ICollection<FilePath> testAssemblies)
    {
        Solution = solution;
        CakeIntellisenseNuSpec = cakeIntellisenseNuSpec;
        TestAssemblies = testAssemblies;
        TestCoverageOutputFilePath = testCoverageOutputFilePath;
    }
}

public class BuildDirectories
{
    public DirectoryPath RootDir { get; private set; }
    public DirectoryPath Artifacts { get; private set; }
    public DirectoryPath TestResults { get; private set; }
    public ICollection<DirectoryPath> TestDirs { get; private set; }
    public ICollection<DirectoryPath> ToClean { get; private set; }

    public BuildDirectories(
        DirectoryPath rootDir,
        DirectoryPath artifacts,
        DirectoryPath testResults,
        ICollection<DirectoryPath> testDirs,
        ICollection<DirectoryPath> toClean)
    {
        RootDir = rootDir;
        Artifacts = artifacts;
        TestDirs = testDirs;
        ToClean = toClean;
        TestResults = testResults;
    }
}

public class BuildPackages
{
    public FilePath NuGetPackage { get; private set;}

    public BuildPackages(FilePath nuGetPackage)
    {
        this.NuGetPackage = nuGetPackage;
    }

    public static BuildPackages GetPackages(BuildPaths paths, BuildVersion version)
    {
        return new BuildPackages(paths.Directories.Artifacts.CombineWithFilePath("Cake.Intellisense."+ version.SemVersion + ".nupkg"));
    }
}