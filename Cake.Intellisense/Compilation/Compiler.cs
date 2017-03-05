using System.IO;
using System.Linq;
using System.Reflection;
using Cake.MetadataGenerator.Reflection;
using Microsoft.CodeAnalysis;
using NLog;

namespace Cake.MetadataGenerator.Compilation
{
    public class Compiler : ICompiler
    {
        private readonly IAssemblyLoader assemblyLoader;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Compiler(IAssemblyLoader assemblyLoader)
        {
            this.assemblyLoader = assemblyLoader;
        }

        public Assembly Compile(Microsoft.CodeAnalysis.Compilation compilation, string outputPath)
        {
            var result = compilation.Emit(outputPath, Path.ChangeExtension(outputPath, "pdb"), Path.ChangeExtension(outputPath, "xml"));

            if (!result.Success)
            {
                var failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (var diagnostic in failures)
                {
                    Logger.Error(diagnostic);
                }

                return null;
            }

            return assemblyLoader.LoadFrom(outputPath);
        }
    }
}