using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.CodeGeneration;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters;
using Cake.MetadataGenerator.NuGet;
using Cake.MetadataGenerator.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NLog;
using NuGet;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator
{
    public class MetadataGenerator : IMetadataGenerator
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICakeSourceGeneratorService cakeSourceGenerator;
        private readonly ICakeSyntaxRewriterService cakeSyntaxRewriterService;
        private readonly INuGetPackageManager nuGetPackageManager;
        private readonly INuGetDependencyResolver dependencyResolver;
        private readonly IPackageAssemblyResolver packageAssemblyResolver;
        private readonly ICompiler compiler;
        private readonly IMetadataReferenceLoader metadataReferenceLoader;
        private readonly IAssemblyLoader assemblyLoader;

        public MetadataGenerator(
            ICakeSourceGeneratorService cakeSourceGenerator,
            ICakeSyntaxRewriterService cakeSyntaxRewriterService,
            INuGetPackageManager nuGetPackageManager,
            INuGetDependencyResolver dependencyResolver,
            IPackageAssemblyResolver packageAssemblyResolver,
            ICompiler compiler,
            IMetadataReferenceLoader metadataReferenceLoader,
            IAssemblyLoader assemblyLoader)
        {
            this.cakeSourceGenerator = cakeSourceGenerator;
            this.cakeSyntaxRewriterService = cakeSyntaxRewriterService;
            this.nuGetPackageManager = nuGetPackageManager;
            this.dependencyResolver = dependencyResolver;
            this.packageAssemblyResolver = packageAssemblyResolver;
            this.compiler = compiler;
            this.metadataReferenceLoader = metadataReferenceLoader;
            this.assemblyLoader = assemblyLoader;
        }

        public GeneratorResult Generate(MetadataGeneratorOptions options)
        {
            var generatorResult = new GeneratorResult();
            var targetFramework = new FrameworkName(options.TargetFramework);
            var package = nuGetPackageManager.InstallPackage(options.Package, options.PackageVersion, targetFramework);

            if (package == null)
            {
                Logger.Error("Unable to find package {0} {1}", options.Package, options.PackageVersion ?? string.Empty);
                return null;
            }

            var packages = dependencyResolver.GetDependentPackagesAndSelf(package, targetFramework);
            var assemblies = packageAssemblyResolver.ResolveAssemblies(package, targetFramework);
            var physicalPackageFiles = GetPackageFiles(packages, targetFramework);


            foreach (var assembly in assemblies)
            {
                var compilationUnit = cakeSourceGenerator.Generate(assembly);
                var rewritenNode = cakeSyntaxRewriterService.Rewrite(compilationUnit, assembly);

                var emitedAssemblyName = $"{assembly.GetName().Name}.{MetadataGeneration.MetadataClassSufix}";
                var compilation = CSharpCompilation.Create(
                   emitedAssemblyName,
                   new[] { ParseSyntaxTree(rewritenNode.NormalizeWhitespace().ToFullString()) },
                   PrepareMetadataReferences(assemblies, physicalPackageFiles),
                   new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                var result = compiler.Compile(compilation, $"{emitedAssemblyName}.dll");
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

            return assemblyReferences.Concat(referencedAssemblyReferences).Concat(physicalPackagesReferences);
        }
    }
}