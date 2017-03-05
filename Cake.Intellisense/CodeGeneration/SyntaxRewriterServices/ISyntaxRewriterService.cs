using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices
{
    public interface ISyntaxRewriterService
    {
        int Order { get; }

        SyntaxNode Rewrite(Assembly assembly, SemanticModel semanticModel, SyntaxNode node);
    }
}