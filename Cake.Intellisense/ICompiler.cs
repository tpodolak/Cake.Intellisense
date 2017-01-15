using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;

namespace Cake.MetadataGenerator
{
    public interface ICompiler
    {
        Assembly Compile(CSharpCompilation compilation, string outputPath);
    }
}