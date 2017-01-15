using Microsoft.CodeAnalysis.CSharp;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriters
{
    public interface ICurrentCompilationContext
    {
        CSharpCompilation Compilation { get; }
    }
}