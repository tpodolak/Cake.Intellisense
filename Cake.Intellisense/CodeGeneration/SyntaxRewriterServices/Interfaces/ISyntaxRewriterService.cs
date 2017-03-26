using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.Interfaces
{
    public interface ISyntaxRewriterService
    {
        int Order { get; }

        SyntaxNode Rewrite(Assembly assembly, SemanticModel semanticModel, SyntaxNode node);
    }
}