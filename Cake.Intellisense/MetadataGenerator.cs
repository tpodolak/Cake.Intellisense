using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.CodeGeneration;
using Cake.MetadataGenerator.CommandLine;
using Cake.MetadataGenerator.Logging;
using Cake.MetadataGenerator.Settings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NLog;
using NuGet;
using IFileSystem = Cake.MetadataGenerator.FileSystem.IFileSystem;
namespace Cake.MetadataGenerator
{
    public class MetadataGenerator : IMetadataGenerator
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICakeMetadataGenerator cakeMetadataGenerator;
        private readonly IArgumentParser argumentParser;
        private readonly INuGetSettings nuGetSettings;
        private readonly IFileSystem fileSystem;
        private readonly IConsoleReader consoleReader;
        private readonly IConsoleWriter consoleWriter;

        public MetadataGenerator(ICakeMetadataGenerator cakeMetadataGenerator,
            IArgumentParser argumentParser,
            INuGetSettings nuGetSettings,
            IFileSystem fileSystem,
            IConsoleReader consoleReader,
            IConsoleWriter consoleWriter)
        {
            this.cakeMetadataGenerator = cakeMetadataGenerator;
            this.argumentParser = argumentParser;
            this.nuGetSettings = nuGetSettings;
            this.fileSystem = fileSystem;
            this.consoleReader = consoleReader;
            this.consoleWriter = consoleWriter;
        }

        public void Generate(string[] args)
        {
            var parserResult = argumentParser.Parse<MetadataGeneratorOptions>(args);

            if (parserResult.Errors.Any())
            {
                Logger.Error("Error while parsing arguments");
                return;
            }

            Generate(parserResult.Result);
        }

        public GeneratorResult Generate(MetadataGeneratorOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.OutputFolder) && !fileSystem.DirectoryExists(options.OutputFolder))
            {
                fileSystem.CreateDirectory(options.OutputFolder);
            }

            var repo =
                (PackageRepositoryBase)PackageRepositoryFactory.Default.CreateRepository(nuGetSettings.PackageSource);
            repo.Logger = new NLogNugetLoggerAdapter(LogManager.GetLogger(repo.GetType().FullName));
            //Get the list of all NuGet packages with ID 'EntityFramework'       
            var packages = repo.FindPackagesById(options.Package).ToList();

            //Filter the list of packages that are not Release (Stable) versions
            packages = packages.Where(item => item.IsReleaseVersion()).ToList();

            var newset = packages.Last();

            var packageManager = new PackageManager(repo, nuGetSettings.LocalRepositoryPath)
            {
                Logger = new NLogNugetLoggerAdapter(LogManager.GetLogger(repo.GetType().FullName))
            };

            var packa = packageManager.LocalRepository.GetPackages().ToList();
            //Download and unzip the package
            packageManager.InstallPackage(newset.Id, newset.Version);
            var local = packageManager.LocalRepository.FindPackage(newset.Id, newset.Version);

            var dependencyId = 0;

            var frameworks = local.GetFiles().GroupBy(val => val.TargetFramework).Select(fm => fm.Key).ToList();

            if (string.IsNullOrEmpty(options.PackageFrameworkTargetVersion))
            {
                consoleWriter.WriteLine("Frameworks");
                for (var index = 0; index < frameworks.Count; index++)
                {
                    var framework = frameworks[index];
                    consoleWriter.WriteLine($"[{index + 1}] - {framework}");
                }

                do
                {
                    consoleWriter.WriteLine("Please select framework");
                }
                while (!consoleReader.TryRead(out dependencyId));

                dependencyId--;
            }
            else
            {
                dependencyId = frameworks.FindIndex(val => val.ToString() == options.PackageFrameworkTargetVersion);
            }

            var targetFramework = frameworks[dependencyId];
            var packges = GetDependentPackagesAndSelf(newset, packa, targetFramework, repo);

            var physicalPackageFiles = packges.SelectMany(
                    f =>
                        f.GetFiles()
                            .OfType<PhysicalPackageFile>()
                            .Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll")))
                .ToList();

            var assemblies =
              newset.GetFiles()
                  .OfType<PhysicalPackageFile>()
                  .Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll"))
                  .Select(val => Assembly.LoadFrom(val.SourcePath))
                  .ToList();

            var metadataReference = assemblies.Select(val => MetadataReference.CreateFromFile(val.Location)).ToList();

            physicalPackageFiles.ForEach(val => Assembly.LoadFrom(val.SourcePath));
            var referencesass =
                physicalPackageFiles
                    .SelectMany(ff => GetReferencesAssemblies(Assembly.LoadFrom(ff.SourcePath)))
                    .Select(val => MetadataReference.CreateFromFile(val.Location))
                    .ToList();

            var more = physicalPackageFiles.Select(val => MetadataReference.CreateFromFile(val.SourcePath)).ToList();

            var result = cakeMetadataGenerator.Generate(assemblies.First());

            var compilation = CSharpCompilation.Create(
               assemblyName: assemblies.First().GetName().Name + ".Metadata",
               syntaxTrees: new[] { SyntaxFactory.ParseSyntaxTree(result.GetRoot().NormalizeWhitespace().ToFullString()) },
               references: referencesass.Union(metadataReference).Union(more),
               options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var compiler = new Compiler().Compile(compilation, assemblies.First().GetName().Name + ".Metadata.dll");

            return new GeneratorResult
            {
                EmitedAssembly = compiler,
                SourceAssemblies = assemblies.ToArray()
            };

        }

        public static List<IPackage> GetDependentPackagesAndSelf(IPackage package, List<IPackage> localPackages, FrameworkName framework, IPackageRepository repo)
        {
            if (package == null)
                return new List<IPackage>();

            return
                new List<IPackage> { package }.Union(package.DependencySets.SelectMany(x => x.Dependencies)
                        .Where(
                            dep =>
                                repo.ResolveDependency(dep, false, true)
                                    .GetFiles().Where(file => file.Path.EndsWith(".dll"))
                                    .Any(file => file.SupportedFrameworks.Contains(framework)))
                        .SelectMany(
                            x =>
                                GetDependentPackagesAndSelf(repo.ResolveDependency(x, false, true), localPackages,
                                    framework, repo)))
                    .ToList();
        }

        public static IEnumerable<Assembly> GetReferencesAssemblies(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies().Select(ReflectionOnlyLoad).Where(val => val != null);
        }

        private static Assembly ReflectionOnlyLoad(AssemblyName assemblyName)
        {
            try
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.FullName == assemblyName.FullName);
                return assembly ?? Assembly.ReflectionOnlyLoad(assemblyName.FullName);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
