using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.Core.Scripting;
using Cake.MetadataGenerator.CodeGeneration;
using Cake.MetadataGenerator.CodeGeneration.LanguageServices;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriters;
using Cake.MetadataGenerator.CommandLine;
using Cake.MetadataGenerator.Documentation;
using Cake.MetadataGenerator.Logging;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NLog;
using NuGet;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator
{
    public class MetadataGenerator : IMetadataGenerator
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly ICSharpCodeGenerationService _codeGenerationService;
        private readonly IArgumentParser _argumentParser;

        public MetadataGenerator(ICSharpCodeGenerationService codeGenerationService, IArgumentParser argumentParser)
        {
            _codeGenerationService = codeGenerationService;
            _argumentParser = argumentParser;
        }

        public void Generate(string[] args)
        {
            var parserResult = _argumentParser.Parse<MetadataGeneratorOptions>(args);

            if (parserResult.Errors.Any())
            {
                logger.Error("Error while parsing arguments");
                return;
            }

            Generate(parserResult.Result);
        }

        public GeneratorResult Generate(MetadataGeneratorOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.OutputFolder) && !Directory.Exists(options.OutputFolder))
            {
                Directory.CreateDirectory(options.OutputFolder);
            }

            PackageRepositoryBase repo =
                (PackageRepositoryBase)PackageRepositoryFactory.Default.CreateRepository(PackageSources.NuGetPackageSource);
            repo.Logger = new NLogNugetLoggerAdapter(LogManager.GetLogger(repo.GetType().FullName));
            //Get the list of all NuGet packages with ID 'EntityFramework'       
            List<IPackage> packages = repo.FindPackagesById(options.Package).ToList();

            //Filter the list of packages that are not Release (Stable) versions
            packages = packages.Where(item => item.IsReleaseVersion()).ToList();

            var newset = packages.Last();

            string path = "C:\\Temp";
            PackageManager packageManager = new PackageManager(repo, path)
            {
                Logger = new NLogNugetLoggerAdapter(LogManager.GetLogger(repo.GetType().FullName))
            };
            var packa = packageManager.LocalRepository.GetPackages().ToList();
            //Download and unzip the package
            packageManager.InstallPackage(newset.Id, newset.Version);
            var local = packageManager.LocalRepository.FindPackage(newset.Id, newset.Version);

            int dependencyId = 0;

            var files = newset.GetFiles();
            var frameworks = files.GroupBy(val => val.TargetFramework).Select(fm => fm.Key).ToList();

            if (string.IsNullOrEmpty(options.PackageFrameworkTargetVersion))
            {
                Console.WriteLine("Frameworks");
                for (var index = 0; index < frameworks.Count; index++)
                {
                    var framework = frameworks[index];
                    Console.WriteLine($"[{index + 1}] - {framework}");
                }

                var key = Console.ReadKey();
                if (int.TryParse(key.KeyChar.ToString(), out dependencyId))
                {
                    dependencyId--;
                }
            }
            else
            {
                dependencyId = frameworks.FindIndex(val => val.ToString() == options.PackageFrameworkTargetVersion);
            }

            var targetFramework = frameworks[dependencyId];
            var packges = GetDependentPackagesAndSelf(newset, packa, targetFramework, repo);


            Console.WriteLine(packges);

            var assemblies =
                newset.GetFiles()
                    .OfType<PhysicalPackageFile>()
                    .Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll"))
                    .Select(val => Assembly.LoadFrom(val.SourcePath))
                    .ToList();


            var metadataReference = assemblies.Select(val => MetadataReference.CreateFromFile(val.Location)).ToList();

            var referencesass =
                packges.SelectMany(
                        f =>
                            f.GetFiles()
                                .OfType<PhysicalPackageFile>()
                                .Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll")))
                    .SelectMany(ff => GetReferencesAssemblies(Assembly.LoadFrom(ff.SourcePath)))
                    .Except(assemblies)
                    .Select(val => MetadataReference.CreateFromFile(val.Location))
                    .ToList();

            var formattableString = $"{assemblies.First().GetName().Name}";

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: formattableString,
                syntaxTrees: new SyntaxTree[] { },
                references: assemblies.Select(y => MetadataReference.CreateFromFile(y.Location)),
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));


            var namespaceSymbols = GetNamespaceMembers(compilation.GlobalNamespace).ToList();

            var compilationSyntax = CompilationUnit();

            foreach (var namespaceSymbol in namespaceSymbols)
            {
                var classSymbols = CreateNamedTypeDeclaration(namespaceSymbol).ToArray();
                if (classSymbols.Any())
                {
                    var namespaceSyntax = CreateNamespace(namespaceSymbol.ToString()).AddMembers(classSymbols);
                    compilationSyntax = compilationSyntax.AddMembers(namespaceSyntax);
                }
            }

            var tree = CSharpSyntaxTree.Create(compilationSyntax);
            compilation = compilation.AddSyntaxTrees(tree);
            var semanticModel = compilation.GetSemanticModel(tree);
            var commentsRewriter = new CommentsSyntaxRewriter(new DocumentationReader(), new XmlCommentProvider(), semanticModel);
            var resxxxx = commentsRewriter.Visit(assemblies.Single(), tree.GetRoot());
            compilation = compilation.ReplaceSyntaxTree(tree, SyntaxTree(resxxxx));
            tree = compilation.SyntaxTrees.FirstOrDefault();
            var rewriter = new ClassSyntaxRewriter();
            var result = rewriter.Visit(tree.GetRoot());
            compilation = compilation.ReplaceSyntaxTree(tree, SyntaxTree(result));

            tree = compilation.SyntaxTrees.First();


            semanticModel = compilation.GetSemanticModel(tree);
            var methodRewriter = new MethodSyntaxRewriter(semanticModel);
            result = methodRewriter.Visit(tree.GetRoot());

            compilation = compilation.ReplaceSyntaxTree(tree, SyntaxTree(result));

            tree = compilation.SyntaxTrees.First();
            var attributeRewriter = new AttributesSyntaxRewriter();
            result = attributeRewriter.Visit(tree.GetRoot());

            var diagnostics = result.GetDiagnostics().ToList();

            compilation = compilation.ReplaceSyntaxTree(tree, SyntaxTree(result));

            compilation = compilation.AddReferences(referencesass);

            var res = new Compiler().Compile(compilation, "someoutput.dll");

            return new GeneratorResult
            {
                EmitedAssembly = res,
                SourceAssemblies = assemblies.ToArray()
            };

        }

        private IEnumerable<ClassDeclarationSyntax> CreateNamedTypeDeclaration(INamespaceOrTypeSymbol namepace)
        {
            return namepace.GetTypeMembers()
                .Where(val => val.Kind == SymbolKind.NamedType && (val.Name == typeof(ScriptHost).Name || val.GetAttributes().Any(x => x.AttributeClass.Name == "CakeAliasCategoryAttribute")))
                .Select(val => _codeGenerationService.CreateNamedTypeDeclaration(val));
        }

        public NamespaceDeclarationSyntax CreateNamespace(string @namespace)
        {
            return NamespaceDeclaration(IdentifierName(@namespace));
        }

        private static IEnumerable<INamespaceOrTypeSymbol> GetNamespaceMembers(INamespaceSymbol symbol)
        {
            yield return symbol;
            if (symbol.GetNamespaceMembers().Any())
            {
                foreach (var innerSymbol in symbol.GetNamespaceMembers())
                {
                    foreach (var reccured in GetNamespaceMembers(innerSymbol))
                    {
                        yield return reccured;
                    }
                }
            }
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
            foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
            {
                yield return Assembly.ReflectionOnlyLoad(assemblyName.FullName);
            }
        }
    }
}
