using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.AttributeRewriters
{
    public class AttributeSyntaxRewriterService : ISyntaxRewriterService
    {
        public int Order { get; } = 3;

        public SyntaxNode Rewrite(Assembly assembly, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new AttributeSyntaxRewriter(assembly);
            return rewriter.Visit(node);
        }
    }
}