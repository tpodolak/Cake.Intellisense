using Microsoft.CodeAnalysis.CSharp;

namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public class CompilationContext : ICurrentCompilationContext
    {
        public CSharpCompilation Compilation { get; }

        public CompilationContext(CSharpCompilation compilation)
        {
            Compilation = compilation;
        }
    }
}