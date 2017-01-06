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

            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

            //Get the list of all NuGet packages with ID 'EntityFramework'       
            List<IPackage> packages = repo.FindPackagesById(options.Package).ToList();

            //Filter the list of packages that are not Release (Stable) versions
            packages = packages.Where(item => item.IsReleaseVersion()).ToList();

            var newset = packages.Last();

            string path = "C:\\Temp";
            PackageManager packageManager = new PackageManager(repo, path);
            var packa = packageManager.LocalRepository.GetPackages().ToList();
            //Download and unzip the package
            packageManager.InstallPackage(newset.Id, newset.Version);

            int dependencyId = 0;

            var packageDependencySets = newset.DependencySets.ToList();
            if (packageDependencySets.Count > 1)
            {
                Console.WriteLine("Multiple farameworks");
                for (var index = 0; index < packageDependencySets.Count; index++)
                {
                    var framework = packageDependencySets[index].TargetFramework;
                    Console.WriteLine($"[{index + 1}] - {framework}");
                }

                var key = Console.ReadKey();
                if (int.TryParse(key.KeyChar.ToString(), out dependencyId))
                {
                    dependencyId--;
                }
            }


            var targetFramework = packageDependencySets[dependencyId].TargetFramework ?? new FrameworkName(".NETFramework", new Version(4, 5));
            var packges = GetDependentPackagesAndSelf(newset, packa, targetFramework);

            var packageInfo = packges.Where(val => Directory.Exists(Path.Combine(path, $"{val.Id.ToString()}.{val.Version}", "lib", ToFolderName(targetFramework)))).Select(val => new
            {
                package = val,
                files = new DirectoryInfo(Path.Combine(path, $"{val.Id.ToString()}.{val.Version}", "lib", ToFolderName(targetFramework))).GetFiles("*.dll", SearchOption.AllDirectories)
            }).ToList();

            var currentPath = packageInfo.Single(val => val.package == newset);

            Console.WriteLine(packges);


            var assemblies = currentPath.files.Select(val => Assembly.LoadFrom(val.FullName)).ToList();

            var referencesass = packageInfo.SelectMany(f => f.files).SelectMany(ff => GetReferencesAssemblies(Assembly.LoadFrom(ff.FullName))).Except(assemblies).Select(val => MetadataReference.CreateFromFile(val.Location)).ToList();

            var zzz = new Class1();
            var types = assemblies.SelectMany(val => val.GetExportedTypes()).Where(val => val == typeof(ScriptHost) || val.GetCustomAttributes<CakeAliasCategoryAttribute>().Any()).ToList();
            var namespaces = string.Join(Environment.NewLine, types.Select(val => $"using static {val.Namespace}.{val.Name}Metadata;"));
            var result = zzz.Genrate(types);
            var x = result.ToFullString();


            var formattableString = $"{options.Package}.AliasesMetadata";
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: formattableString,
                syntaxTrees: new[] { CSharpSyntaxTree.ParseText(x) },
                references: referencesass.Union(packageInfo.SelectMany(val => val.files.Select(y => MetadataReference.CreateFromFile(y.FullName)))),
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            zzz.Compile(compilation, Path.ChangeExtension(formattableString, ".dll"));

            Console.ReadKey();


        }

        public static List<IPackage> GetDependentPackagesAndSelf(IPackage package, List<IPackage> localPackages, FrameworkName framework)
        {
            if (package == null)
                return new List<IPackage>();

            return
                new List<IPackage> { package }.Union(package.DependencySets.Where(val => val.TargetFramework == framework)
                    .SelectMany(
                        val =>
                            val.Dependencies.SelectMany(
                                x =>
                                    GetDependentPackagesAndSelf(localPackages.SingleOrDefault(pack => pack.Id == x.Id), localPackages,
                                        framework))))
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
