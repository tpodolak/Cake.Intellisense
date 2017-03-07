using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.AttributeRewriters
{
    public class AttributeSyntaxRewriterService : ISyntaxRewriterService
    {
        private static readonly string[] AttributesToRemove =
        {
            CakeAttributeNames.CakeAliasCategory,
            CakeAttributeNames.CakeMethodAlias,
            CakeAttributeNames.CakeNamespaceImport,
            CakeAttributeNames.CakePropertyAlias
        };

        public int Order { get; } = 3;

        public SyntaxNode Rewrite(Assembly assembly, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new AttributeSyntaxRewriter(AttributesToRemove);
            return rewriter.Visit(node);
        }
    }
}