using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using Cake.Intellisense.CodeGeneration.SourceGenerators;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters;
using Cake.Intellisense.Compilation;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NLog;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
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
            this.compilationProvider = compilationProvider;
            this.fileSystem = fileSystem;
        }

        public GeneratorResult Generate(MetadataGeneratorOptions options)
        {
            if (options == null)
            {
                Logger.Fatal("Unable to parse arguments");
                return null;
            }

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

            foreach (var assembly in assemblies)
            {
                var compilationUnit = cakeSourceGenerator.Generate(assembly);
                var rewrittenNode = cakeSyntaxRewriterService.Rewrite(compilationUnit, assembly);

                var emitedAssemblyName = $"{assembly.GetName().Name}.{MetadataGeneration.MetadataClassSuffix}";
                var compilation = compilationProvider.Get(
                   emitedAssemblyName,
                   new[] { ParseSyntaxTree(rewrittenNode.NormalizeWhitespace().ToFullString()) },
                   metadataReferenceLoader.CreateFromPackages(packages, targetFramework),
                   new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                var result = compiler.Compile(compilation, Path.Combine(options.OutputFolder ?? string.Empty, $"{emitedAssemblyName}.dll"));
                generatorResult.EmitedAssemblies.Add(result);
                generatorResult.SourceAssemblies.Add(assembly);
            }

            return generatorResult;
        }
    }
}