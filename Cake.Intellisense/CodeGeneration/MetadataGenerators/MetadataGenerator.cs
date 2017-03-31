using System;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using Cake.Intellisense.CodeGeneration.MetadataGenerators.Interfaces;
using Cake.Intellisense.CodeGeneration.SourceGenerators.Interfaces;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters.Interfaces;
using Cake.Intellisense.CommandLine;
using Cake.Intellisense.Compilation.Interfaces;
using Cake.Intellisense.NuGet.Interfaces;
using Cake.Intellisense.Reflection.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NLog;
using static Cake.Intellisense.Constants.MetadataGeneration;
using IDependencyResolver = Cake.Intellisense.NuGet.Interfaces.IDependencyResolver;
using IFileSystem = Cake.Intellisense.FileSystem.Interfaces.IFileSystem;
using IPackageManager = Cake.Intellisense.NuGet.Interfaces.IPackageManager;

namespace Cake.Intellisense.CodeGeneration.MetadataGenerators
{
    public class MetadataGenerator : IMetadataGenerator
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICakeSourceGeneratorService _cakeSourceGenerator;
        private readonly ICakeSyntaxRewriterService _cakeSyntaxRewriterService;
        private readonly IPackageManager _packageManager;
        private readonly IDependencyResolver _dependencyResolver;
        private readonly IPackageAssemblyResolver _packageAssemblyResolver;
        private readonly ICompiler _compiler;
        private readonly IMetadataReferenceLoader _metadataReferenceLoader;
        private readonly ICompilationProvider _compilationProvider;
        private readonly IFileSystem _fileSystem;

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
            _cakeSourceGenerator = cakeSourceGenerator ?? throw new ArgumentNullException(nameof(cakeSourceGenerator));
            _cakeSyntaxRewriterService = cakeSyntaxRewriterService ?? throw new ArgumentNullException(nameof(cakeSyntaxRewriterService));
            _packageManager = packageManager ?? throw new ArgumentNullException(nameof(packageManager));
            _dependencyResolver = dependencyResolver ?? throw new ArgumentNullException(nameof(dependencyResolver));
            _packageAssemblyResolver = packageAssemblyResolver ?? throw new ArgumentNullException(nameof(packageAssemblyResolver));
            _compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
            _metadataReferenceLoader = metadataReferenceLoader ?? throw new ArgumentNullException(nameof(metadataReferenceLoader));
            _compilationProvider = compilationProvider ?? throw new ArgumentNullException(nameof(compilationProvider));
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public GeneratorResult Generate(MetadataGeneratorOptions options)
        {
            var generatorResult = new GeneratorResult();
            var targetFramework = new FrameworkName(options.TargetFramework);
            var package = _packageManager.InstallPackage(options.Package, options.PackageVersion, targetFramework);

            if (package == null)
            {
                Logger.Error("Unable to find package {0} {1}", options.Package, options.PackageVersion ?? string.Empty);
                return null;
            }

            if (!string.IsNullOrWhiteSpace(options.OutputFolder) && !_fileSystem.DirectoryExists(options.OutputFolder))
                _fileSystem.CreateDirectory(options.OutputFolder);

            var packages = _dependencyResolver.GetDependenciesAndSelf(package, targetFramework).ToList();
            var assemblies = _packageAssemblyResolver.ResolveAssemblies(package, targetFramework);

            foreach (var assembly in assemblies)
            {
                var compilationUnit = _cakeSourceGenerator.Generate(assembly);
                var rewrittenNode = _cakeSyntaxRewriterService.Rewrite(compilationUnit, assembly);

                var emitedAssemblyName = $"{assembly.GetName().Name}.{MetadataClassSuffix}";
                var compilation = _compilationProvider.Get(
                   emitedAssemblyName,
                   new[] { SyntaxFactory.ParseSyntaxTree(rewrittenNode.NormalizeWhitespace().ToFullString()) },
                   _metadataReferenceLoader.CreateFromPackages(packages, targetFramework),
                   new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                var result = _compiler.Compile(compilation, Path.Combine(options.OutputFolder ?? string.Empty, $"{emitedAssemblyName}.dll"));
                generatorResult.EmitedAssemblies.Add(result);
                generatorResult.SourceAssemblies.Add(assembly);
            }

            return generatorResult;
        }
    }
}