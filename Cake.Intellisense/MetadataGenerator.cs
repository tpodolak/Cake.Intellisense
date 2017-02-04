using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.MetadataGenerator.CodeGeneration;
using Cake.MetadataGenerator.CommandLine;
using Cake.MetadataGenerator.NuGet;
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
        private readonly INuGetPackageManager nuGetPackageManager;
        private readonly INuGetDependencyResolver dependencyResolver;
        private readonly IFileSystem fileSystem;
        private readonly IConsoleReader consoleReader;
        private readonly IConsoleWriter consoleWriter;

        public MetadataGenerator(ICakeMetadataGenerator cakeMetadataGenerator,
            IArgumentParser argumentParser,
            INuGetPackageManager nuGetPackageManager,
            INuGetDependencyResolver dependencyResolver,
            IFileSystem fileSystem,
            IConsoleReader consoleReader,
            IConsoleWriter consoleWriter)
        {
            this.cakeMetadataGenerator = cakeMetadataGenerator;
            this.argumentParser = argumentParser;
            this.nuGetPackageManager = nuGetPackageManager;
            this.dependencyResolver = dependencyResolver;
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
                fileSystem.CreateDirectory(options.OutputFolder);

            var package = nuGetPackageManager.InstallPackage(options.Package, options.PackageVersion);

            if (package == null)
            {
                Logger.Error("Unable to find package {0} {1}", options.Package, options.PackageVersion ?? string.Empty);
                return null;
            }

            var frameworks = nuGetPackageManager.GetTargetFrameworks(package);
            int dependencyId;
            if (string.IsNullOrEmpty(options.PackageFrameworkTargetVersion))
            {
                consoleWriter.WriteLine("Frameworks");
                for (var index = 0; index < frameworks.Count; index++)
                {
                    var framework = frameworks[index];
                    consoleWriter.WriteLine($"[{index}] - {framework}");
                }

                do
                {
                    consoleWriter.WriteLine("Please select framework");
                }
                while (!consoleReader.TryRead(out dependencyId));
            }
            else
            {
                dependencyId = frameworks.FindIndex(val => val.ToString() == options.PackageFrameworkTargetVersion);
            }

            var targetFramework = frameworks[dependencyId];
            var packges = dependencyResolver.GetDependentPackagesAndSelf(package, targetFramework);

            var physicalPackageFiles = packges.SelectMany(
                    f =>
                        f.GetFiles()
                            .OfType<PhysicalPackageFile>()
                            .Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll")))
                .ToList();

            var assemblies =
              package.GetFiles()
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
