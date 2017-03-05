using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Cake.MetadataGenerator.Compilation
{
    public interface ICompilationProvider
    {
        Microsoft.CodeAnalysis.Compilation Get(
            string assemblyName,
            IEnumerable<SyntaxTree> syntaxTrees = null,
            IEnumerable<MetadataReference> references = null,
            CSharpCompilationOptions options = null);
    }
}