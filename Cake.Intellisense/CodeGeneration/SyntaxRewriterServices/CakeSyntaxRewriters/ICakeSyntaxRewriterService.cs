using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters
{
    public interface ICakeSyntaxRewriterService
    {
        SyntaxNode Rewrite(CompilationUnitSyntax compilationUnitSyntax, Assembly assembly);
    }
}