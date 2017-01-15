using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NLog;

namespace Cake.MetadataGenerator
{
    public class Compiler : ICompiler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Assembly Compile(CSharpCompilation compilation, string outputPath)
        {
            var result = compilation.Emit(outputPath);

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

            return Assembly.LoadFrom(outputPath);
        }
    }
}