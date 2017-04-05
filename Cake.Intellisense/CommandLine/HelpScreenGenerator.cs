using System.Linq;
using System.Reflection;
using Cake.Intellisense.CommandLine.Interfaces;
using CommandLine.Text;

namespace Cake.Intellisense.CommandLine
{
    public class HelpScreenGenerator : IHelpScreenGenerator
    {
        public string Generate<T>() where T : class, new()
        {
            var assembly = typeof(T).Assembly;
            var assemblyAttributes = assembly.GetCustomAttributes().ToList();

            var helpText = HelpText.AutoBuild(new T());
            var assemblyTitle = assemblyAttributes.OfType<AssemblyTitleAttribute>().Single().Title;
            var assemblyVersion = assemblyAttributes.OfType<AssemblyInformationalVersionAttribute>().Single().InformationalVersion;

            helpText.Heading = $"{assemblyTitle} {assemblyVersion}";
            helpText.Copyright = assemblyAttributes.OfType<AssemblyCopyrightAttribute>().Single().Copyright;
            return helpText;
        }
    }
}