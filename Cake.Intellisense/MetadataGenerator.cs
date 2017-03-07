using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.Intellisense.CodeGeneration.SourceGenerators;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters;
using Cake.Intellisense.Compilation;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MoreLinq;
using NLog;
using NuGet;
using IDependencyResolver = Cake.Intellisense.NuGet.IDependencyResolver;
using IFileSystem = Cake.Intellisense.FileSystem.IFileSystem;
using IPackageManager = Cake.Intellisense.NuGet.IPackageManager;

namespace Cake.Intellisense
{
    public class MetadataGenerator : IMetadataGenerator
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICakeSourceGeneratorService cakeSourceGenerator;
        private readonly ICakeSyntaxRewriterService cakeSyntaxRewriterService;
        private readonly IPackageManager packageManager;
        private readonly IDependencyResolver dependencyResolver;
        private readonly IPackageAssemblyResolver packageAssemblyResolver;
        private readonly ICompiler compiler;
        private readonly IMetadataReferenceLoader metadataReferenceLoader;
        private readonly IAssemblyLoader assemblyLoader;
        private readonly ICompilationProvider compilationProvider;
        private readonly IFileSystem fileSystem;

        public MetadataGenerator(
            ICakeSourceGeneratorService cakeSourceGenerator,
            ICakeSyntaxRewriterService cakeSyntaxRewriterService,
            IPackageManager packageManager,
            IDependencyResolver dependencyResolver,
            IPackageAssemblyResolver packageAssemblyResolver,
            ICompiler compiler,
            IMetadataReferenceLoader metadataReferenceLoader,
            IAssemblyLoader assemblyLoader,
            ICompilationProvider compilationProvider,
            IFileSystem fileSystem)
        {
            this.cakeSourceGenerator = cakeSourceGenerator;
            this.cakeSyntaxRewriterService = cakeSyntaxRewriterService;
            this.packageManager = packageManager;
            this.dependencyResolver = dependencyResolver;
            this.packageAssemblyResolver = packageAssemblyResolver;
            this.compiler = compiler;
            this.metadataReferenceLoader = metadataReferenceLoader;
            this.assemblyLoader = assemblyLoader;
            this.compilationProvider = compilationProvider;
            this.fileSystem = fileSystem;
        }

        public GeneratorResult Generate(MetadataGeneratorOptions options)
        {
            var generatorResult = new GeneratorResult();
            var targetFramework = new FrameworkName(options.TargetFramework);
            var package = packageManager.InstallPackage(options.Package, options.PackageVersion, targetFramework);

            if (package == null)
            {
                Logger.Error("Unable to find package {0} {1}", options.Package, options.PackageVersion ?? string.Empty);
                return null;
            }

            if (!string.IsNullOrWhiteSpace(options.OutputFolder) && !fileSystem.DirectoryExists(options.OutputFolder))
                fileSystem.CreateDirectory(options.OutputFolder);

            var packages = dependencyResolver.GetDependentPackagesAndSelf(package, targetFramework).ToList();
            var assemblies = packageAssemblyResolver.ResolveAssemblies(package, targetFramework);
            var physicalPackageFiles = GetPackageFiles(packages, targetFramework);

            foreach (var assembly in assemblies)
            {
                var compilationUnit = cakeSourceGenerator.Generate(assembly);
                var rewrittenNode = cakeSyntaxRewriterService.Rewrite(compilationUnit, assembly);

                var emitedAssemblyName = $"{assembly.GetName().Name}.{MetadataGeneration.MetadataClassSuffix}";
                var compilation = compilationProvider.Get(
                   emitedAssemblyName,
                   new[] { SyntaxFactory.ParseSyntaxTree(rewrittenNode.NormalizeWhitespace().ToFullString()) },
                   PrepareMetadataReferences(assemblies, physicalPackageFiles),
                   new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                var result = compiler.Compile(compilation, Path.Combine(options.OutputFolder ?? string.Empty, $"{emitedAssemblyName}.dll"));
                generatorResult.EmitedAssemblies.Add(result);
                generatorResult.SourceAssemblies.Add(assembly);
            }

            return generatorResult;
        }

        private List<IPackageFile> GetPackageFiles(List<IPackage> packages, FrameworkName targetFramework)
        {
            return packages.SelectMany(file => GetPackageFiles(file, targetFramework)).ToList();
        }

        private IEnumerable<IPackageFile> GetPackageFiles(IPackage file, FrameworkName targetFramework)
        {
            return file.GetFiles().Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll"));
        }

        private IEnumerable<PortableExecutableReference> PrepareMetadataReferences(List<Assembly> assemblies, List<IPackageFile> physicalPackageFiles)
        {
            physicalPackageFiles.ForEach(val => assemblyLoader.LoadFrom(((PhysicalPackageFile)val).SourcePath));

            var assemblyReferences = assemblies.Select(val => metadataReferenceLoader.CreateFromFile(val.Location));

            var referencedAssemblyReferences =
                physicalPackageFiles
                    .SelectMany(packageFile => assemblyLoader.LoadReferencedAssemblies(((PhysicalPackageFile)packageFile).SourcePath))
                    .Select(assembly => metadataReferenceLoader.CreateFromFile(assembly.Location))
                    .ToList();

            var physicalPackagesReferences = physicalPackageFiles.Select(val => metadataReferenceLoader.CreateFromFile(((PhysicalPackageFile)val).SourcePath));

            return assemblyReferences.Union(referencedAssemblyReferences).Union(physicalPackagesReferences).DistinctBy(reference => Path.GetFileName(reference.FilePath));
        }
    }
}