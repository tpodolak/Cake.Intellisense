using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Annotations;
using Microsoft.CodeAnalysis;

namespace Cake.Intellisense
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // var x = AssemblyMetadata.CreateFromFile("Cake.Common.dll");

            var zzz = new Class1();
            zzz.Foo();

//            ShowX();
//            Console.WriteLine();
//            var assembly = Assembly.LoadFrom("Cake.Common.dll");
//            foreach (var type in assembly.GetTypes().SelectMany(val => val.GetMethods().Where(x => x.GetCustomAttributes<CakeMethodAliasAttribute>().Any())))
//            {
//                ShowMethods(type);
//
//            }

            Console.ReadKey();

        }


        static partial void ShowX();

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

            return  t == typeof(void) ? "void" : x + t.Name;
        }
    }
}
