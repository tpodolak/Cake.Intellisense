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
            var physicalPackageFiles = GetPhysicalPackages(packages, targetFramework);


            foreach (var assembly in assemblies)
            {
                var compilationUnit = cakeSourceGenerator.Generate(assemblies.First());
                var rewritenNode = cakeSyntaxRewriterService.Rewrite(compilationUnit, assembly);

                var compilation = CSharpCompilation.Create(
                   assemblyName: assembly.GetName().Name + ".Metadata",
                   syntaxTrees: new[] { ParseSyntaxTree(rewritenNode.NormalizeWhitespace().ToFullString()) },
                   references: PrepareMetadataReferences(assemblies, physicalPackageFiles),
                   options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                var result = compiler.Compile(compilation, assemblies.First().GetName().Name + ".Metadata.dll");
                generatorResult.EmitedAssemblies.Add(result);
                generatorResult.SourceAssemblies.Add(assembly);

            }

            return generatorResult;
        }

        private static List<PhysicalPackageFile> GetPhysicalPackages(List<IPackage> packages, FrameworkName targetFramework)
        {
            return packages.SelectMany(file => GetPhysicalPackages(file, targetFramework)).ToList();
        }

        private static IEnumerable<PhysicalPackageFile> GetPhysicalPackages(IPackage file, FrameworkName targetFramework)
        {
            return file.GetFiles().OfType<PhysicalPackageFile>().Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll"));
        }

        private IEnumerable<PortableExecutableReference> PrepareMetadataReferences(List<Assembly> assemblies, List<PhysicalPackageFile> physicalPackageFiles)
        {
            physicalPackageFiles.ForEach(val => assemblyLoader.LoadFrom(val.SourcePath));

            var assemblyReferences = assemblies.Select(val => metadataReferenceLoader.CreateFromFile(val.Location));

            var referencedAssemblyReferences =
                physicalPackageFiles
                    .SelectMany(ff => assemblyLoader.LoadReferencedAssemblies(ff.SourcePath))
                    .Select(val => metadataReferenceLoader.CreateFromFile(val.Location))
                    .ToList();

            var physicalPackagesReferences = physicalPackageFiles.Select(val => metadataReferenceLoader.CreateFromFile(val.SourcePath));

            return assemblyReferences.Union(referencedAssemblyReferences).Union(physicalPackagesReferences);
        }
    }
}
