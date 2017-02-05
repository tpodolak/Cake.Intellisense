using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.AttributeRewriters
{
    public class AttributeSyntaxRewriterService : ISyntaxRewriterService
    {
        private static readonly string[] AttributesToRemove =
        {
            CakeAttributes.CakeAliasCategory,
            CakeAttributes.CakeMethodAlias,
            CakeAttributes.CakeNamespaceImport,
            CakeAttributes.CakePropertyAlias
        };

        public int Order { get; } = 3;

        public SyntaxNode Rewrite(Assembly assemlby, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new AttributeSyntaxRewriter(AttributesToRemove);
            return rewriter.Visit(node);
        }
    }
}