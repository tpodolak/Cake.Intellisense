using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters
{
    public interface ICakeSyntaxRewriterService
    {
        SyntaxNode Rewrite(CompilationUnitSyntax compilationUnitSyntax, Assembly assembly);
    }
}