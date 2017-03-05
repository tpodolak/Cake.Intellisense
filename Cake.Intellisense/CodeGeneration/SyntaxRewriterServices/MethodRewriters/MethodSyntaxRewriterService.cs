using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.MethodRewriters
{
    public class MethodSyntaxRewriterService : ISyntaxRewriterService
    {
        public int Order { get; } = 2;

        public SyntaxNode Rewrite(Assembly assembly, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new MethodSyntaxRewriter(semanticModel);
            return rewriter.Visit(node).NormalizeWhitespace();
        }
    }
}