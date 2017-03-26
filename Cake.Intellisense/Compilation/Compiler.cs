using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Cake.Intellisense.Compilation.Interfaces;
using Cake.Intellisense.FileSystem.Interfaces;
using Cake.Intellisense.Reflection.Interfaces;
using Microsoft.CodeAnalysis;
using NLog;

namespace Cake.Intellisense.Compilation
{
    public class Compiler : ICompiler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IAssemblyLoader _assemblyLoader;
        private readonly IFileSystem _fileSystem;

        public Compiler(IAssemblyLoader assemblyLoader, IFileSystem fileSystem)
        {
            _assemblyLoader = assemblyLoader ?? throw new ArgumentNullException(nameof(assemblyLoader));
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public Assembly Compile(Microsoft.CodeAnalysis.Compilation compilation, string outputPath)
        {
            var pdbPath = Path.ChangeExtension(outputPath, "pdb");
            var xmlDocPath = Path.ChangeExtension(outputPath, "xml");

            using (MemoryStream dllStream = new MemoryStream(), pdbStream = new MemoryStream(), xmlStream = new MemoryStream())
            {
                using (var win32ResStream = compilation.CreateDefaultWin32Resources(
                    versionResource: true, // Important!
                    noManifest: false,
                    manifestContents: null,
                    iconInIcoFormat: null))
                {
                    var result = compilation.Emit(
                        peStream: dllStream,
                        pdbStream: pdbStream,
                        xmlDocumentationStream: xmlStream,
                        win32Resources: win32ResStream);

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

                    _fileSystem.WriteAllBytes(outputPath, dllStream.ToArray());
                    _fileSystem.WriteAllBytes(pdbPath, pdbStream.ToArray());
                    _fileSystem.WriteAllBytes(xmlDocPath, xmlStream.ToArray());
                    return _assemblyLoader.LoadFrom(outputPath);
                }
            }
        }
    }
}