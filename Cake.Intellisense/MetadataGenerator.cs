using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading;
using Cake.Core.Annotations;
using Cake.Core.Scripting;
using Cake.MetadataGenerator.CommandLine;
using Cake.MetadataGenerator.Logging;
using CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Host;
using NLog;
using NuGet;
using AssemblyMetadata = Microsoft.CodeAnalysis.AssemblyMetadata;
using Microsoft.CodeAnalysis.Shared.Extensions;

namespace Cake.MetadataGenerator
{
    public class MetadataGenerator
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void Generate(string[] args)
        {
            MetadataGeneratorOptions options = null;
            IEnumerable<Error> errorList = new List<Error>();
            // args = new[] { "" };
            var parser = new Parser(val => val.HelpWriter = Console.Out);
            var parserResult = parser.ParseArguments<MetadataGeneratorOptions>(args);
            parserResult.WithParsed(lineOptions => options = lineOptions);
            parserResult.WithNotParsed(errors => errorList = errors.ToList());

            if (errorList.Any())
            {
                return;
            }

            Generate(options);
        }

        public GeneratorResult Generate(MetadataGeneratorOptions options)
        {



            if (!string.IsNullOrWhiteSpace(options.OutputFolder) && !Directory.Exists(options.OutputFolder))
            {
                Directory.CreateDirectory(options.OutputFolder);
            }

            PackageRepositoryBase repo =
                (PackageRepositoryBase)PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
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


            var zzz = new Class1();
            var types =
                assemblies.SelectMany(val => val.GetExportedTypes())
                    .Where(
                        val =>
                            val.FullName == typeof(ScriptHost).FullName ||
                            val.GetCustomAttributes<CakeAliasCategoryAttribute>().Any())
                    .Select(val => val)
                    .ToList();
            var namespaces = string.Join(Environment.NewLine,
                types.Select(val => $"using static {val.Namespace}.{val.Name}Metadata;"));
            var result = zzz.Genrate(types);
            var x = result.ToFullString();
            logger.Debug(x);
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

            var formattableString = $"{options.Package}.AliasesMetadata";
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: formattableString,
                syntaxTrees: new SyntaxTree[0] { },
                references: referencesass.Union(assemblies.Select(y => MetadataReference.CreateFromFile(y.Location))),
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));


            zzz.Generate(compilation);

            var symbols = compilation.GlobalNamespace.GetNamespaceMembers().ToList();
            var member = symbols[3].GetMembers().ToList()[1].GetTypeMembers()[4].GetMembers()[0];
            var memberComment = member.GetDocumentationCommentXml();
            var memberCommentId = member.GetDocumentationCommentId();
            var someStuff = typeof(SymbolFinder).Assembly.GetType("Microsoft.CodeAnalysis.Shared.Extensions.ICompilationExtensions");
            var type = Type.GetType("Microsoft.CodeAnalysis.Shared.Extensions.ICompilationExtensions");
            var method = someStuff.GetMethods(BindingFlags.Public | BindingFlags.Static).SingleOrDefault(val => val.Name == "GetReferencedAssemblySymbols");

            var assemblySymbols = (IEnumerable<IAssemblySymbol>)method.Invoke(null, new[] { compilation });
            var refsf = compilation.GetMetadataReference(compilation.Assembly);
            var emitedAssemlby = zzz.Compile(compilation, Path.Combine(options.OutputFolder ?? string.Empty, $"{formattableString}.dll"));
            var assemblySymbol = assemblySymbols.ToList()[4];
            var typeNames = assemblySymbol.TypeNames.ToList();
            var comments = assemblySymbol.GetDocumentationCommentXml();
            var someType = assemblySymbol.GetTypeByMetadataName("Cake.SqlServer" + "." + typeNames[5]);
            var members = someType.GetMembers();
            var firstMember = members[0];
            var methodDeclarationSymbol = (IMethodSymbol)members.First(val => val.Kind == SymbolKind.Method);

            var workspace = new AdhocWorkspace().AddSolution(SolutionInfo.Create(SolutionId.CreateNewId("MySolution"),
                    VersionStamp.Default))
                .AddProject("MyProject", "MyAssemblyName", LanguageNames.CSharp)
                .AddMetadataReferences(metadataReference);

            var hostLanguageServices = workspace.Solution.Projects.First().LanguageServices;

            var host = nameof(hostLanguageServices.GetService);
            var iservice = typeof(ILanguageService).Assembly.GetType("Microsoft.CodeAnalysis.CodeGeneration.ICodeGenerationService");

            var cos = hostLanguageServices.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance).SingleOrDefault(val => val.Name == host);
            var csharpHostLanguageService = cos.MakeGenericMethod(iservice).Invoke(hostLanguageServices, null);


            var metadata = (AssemblyMetadata)metadataReference[0].GetMetadata();
            var methodDeclaration = csharpHostLanguageService.GetType().GetMethods().Single(val => val.Name == "CreateMethodDeclaration");
            var typeDeclaration = csharpHostLanguageService.GetType().GetMethods().Single(val => val.Name == "CreateNamedTypeDeclaration");
            var namespaceDeclaration = csharpHostLanguageService.GetType().GetMethods().Single(val => val.Name == "CreateNamespaceDeclaration");
            var methodSyntax = (SyntaxNode)methodDeclaration.Invoke(csharpHostLanguageService, new object[] { methodDeclarationSymbol, 0, null });
            var resultxxx = methodSyntax.NormalizeWhitespace().ToFullString();

            var typeResult = (SyntaxNode)typeDeclaration.Invoke(csharpHostLanguageService, new object[] { someType, 0, null, CancellationToken.None });
            var typeResultXxx = typeResult.NormalizeWhitespace().ToFullString();

            var namespaceResult = (SyntaxNode)namespaceDeclaration.Invoke(csharpHostLanguageService, new object[] { assemblySymbol.GlobalNamespace, 0, null, CancellationToken.None });
            var namespaceFormat = namespaceResult.NormalizeWhitespace().ToFullString();
            return new GeneratorResult
            {
                EmitedAssembly = emitedAssemlby,
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


        static void ShowMethods(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var parameterDescriptions = string.Join
                (", ", method.GetParameters()
                             .Select(x => PrettyTypeName(x.ParameterType) + " " + x.Name)
                             .ToArray());

            Console.WriteLine("{0} {1} ({2})",
                              PrettyTypeName(method.ReturnType),
                              PrettyTypeName(method),
                              parameterDescriptions);

        }

        public static string GetOriginalName(Type type)
        {
            string TypeName = type.FullName.Replace(type.Namespace + ".", "");//Removing the namespace

            var provider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("CSharp"); //You can also use "VisualBasic"
            var reference = new System.CodeDom.CodeTypeReference(TypeName);
            return provider.GetTypeOutput(reference);
        }

        static string PrettyTypeName(MethodInfo t)
        {
            if (t.IsGenericMethod)
            {
                return $"{t.Name}<{string.Join(", ", t.GetGenericArguments().Select(PrettyTypeName))}>";
            }

            return t.Name;
        }

        static string PrettyTypeName(Type t)
        {
            var x = !string.IsNullOrWhiteSpace(t.FullName) ? t.Namespace + "." : string.Empty;

            if (t.IsGenericType)
            {
                return $"{x + t.Name.Substring(0, t.Name.LastIndexOf("`", StringComparison.InvariantCulture))}<{string.Join(", ", t.GetGenericArguments().Select(PrettyTypeName))}>";
            }

            return t == typeof(void) ? "void" : x + t.Name;
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
