using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Annotations;
using Cake.Core.Scripting;
using CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NLog;
using NuGet;

namespace Cake.Intellisense
{
    partial class Program
    {
        static void Main(string[] args)
        {
            CommandLineOptions options = null;
            IEnumerable<Error> errorList = new List<Error>();
            // args = new[] { "" };
            var parser = new Parser(val => val.HelpWriter = Console.Out);
            var parserResult = parser.ParseArguments<CommandLineOptions>(args);
            parserResult.WithParsed(lineOptions => options = lineOptions);
            parserResult.WithNotParsed(errors => errorList = errors.ToList());

            if (errorList.Any())
            {
                return;
            }

            PackageRepositoryBase repo = (PackageRepositoryBase)PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            repo.Logger = new NLogNugetAdapterLogger(LogManager.GetLogger(repo.GetType().FullName));
            //Get the list of all NuGet packages with ID 'EntityFramework'       
            List<IPackage> packages = repo.FindPackagesById(options.Package).ToList();

            //Filter the list of packages that are not Release (Stable) versions
            packages = packages.Where(item => item.IsReleaseVersion()).ToList();

            var newset = packages.Last();

            string path = "C:\\Temp";
            PackageManager packageManager = new PackageManager(repo, path);
            packageManager.Logger = new NLogNugetAdapterLogger(LogManager.GetLogger(repo.GetType().FullName));
            var packa = packageManager.LocalRepository.GetPackages().ToList();
            //Download and unzip the package
            packageManager.InstallPackage(newset.Id, newset.Version);
            var local = packageManager.LocalRepository.FindPackage(newset.Id, newset.Version);

            int dependencyId = 0;

            var files = newset.GetFiles();
            var frameworks = files.GroupBy(val => val.TargetFramework).Select(fm => fm.Key).ToList();



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

            var targetFramework = frameworks[dependencyId];
            var packges = GetDependentPackagesAndSelf(newset, packa, targetFramework, repo);


            Console.WriteLine(packges);


            var assemblies = newset.GetFiles().OfType<PhysicalPackageFile>().Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll")).Select(val => Assembly.LoadFrom(val.SourcePath)).ToList();


            var zzz = new Class1();
            var types = assemblies.SelectMany(val => val.GetExportedTypes()).Where(val => val.FullName == typeof(ScriptHost).FullName || val.GetCustomAttributes<CakeAliasCategoryAttribute>().Any()).Select(val => val).ToList();
            var namespaces = string.Join(Environment.NewLine, types.Select(val => $"using static {val.Namespace}.{val.Name}Metadata;"));
            var result = zzz.Genrate(types);
            var x = result.ToFullString();

            var referencesass = packges.SelectMany(f => f.GetFiles().OfType<PhysicalPackageFile>().Where(val => val.SupportedFrameworks.Contains(targetFramework) && val.Path.EndsWith(".dll"))).SelectMany(ff => GetReferencesAssemblies(Assembly.LoadFrom(ff.SourcePath))).Except(assemblies).Select(val => MetadataReference.CreateFromFile(val.Location)).ToList();

            var formattableString = $"{options.Package}.AliasesMetadata";
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: formattableString,
                syntaxTrees: new[] { CSharpSyntaxTree.ParseText(x) },
                references: referencesass.Union(assemblies.Select(y => MetadataReference.CreateFromFile(y.Location))),
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            zzz.Compile(compilation, $"{formattableString}.dll");

            Console.ReadKey();


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
                                    .GetFiles()
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

        private static string ToFolderName(FrameworkName frameworkName)
        {
            var name = string.Empty;
            if (frameworkName.Identifier == ".NETFramework")
                name = "net";
            if (frameworkName.Identifier == ".NETStandard")
                return "netstandard" + frameworkName.Version;
            return name + frameworkName.Version.ToString().Replace(".", "");
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
