using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;

namespace Cake.MetadataGenerator.Compilation
{
    public interface ICompiler
    {
        Assembly Compile(CSharpCompilation compilation, string outputPath);
    }
}