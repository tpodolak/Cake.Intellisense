using Cake.MetadataGenerator.CommandLine;
using Microsoft.CodeAnalysis.CSharp;

namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public interface ICurrentCompilationContext
    {
        CSharpCompilation Compilation { get; }
    }
}