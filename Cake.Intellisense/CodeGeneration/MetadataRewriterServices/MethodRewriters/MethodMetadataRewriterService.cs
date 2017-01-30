using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.MethodRewriters
{
    public class MethodMetadataRewriterService : IMetadataRewriterService
    {
        public int Order { get; } = 2;

        public SyntaxNode Rewrite(Assembly assemlby, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new MethodSyntaxRewriter(semanticModel);
            return rewriter.Visit(node).NormalizeWhitespace();
        }
    }
}