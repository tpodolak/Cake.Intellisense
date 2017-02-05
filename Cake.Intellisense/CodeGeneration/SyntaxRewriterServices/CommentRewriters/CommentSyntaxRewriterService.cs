using System.Reflection;
using Cake.MetadataGenerator.Documentation;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.CommentRewriters
{
    public class CommentSyntaxRewriterService : ISyntaxRewriterService
    {
        private readonly IDocumentationReader documentationReader;
        private readonly ICommentProvider commentProvider;

        public CommentSyntaxRewriterService(IDocumentationReader documentationReader, ICommentProvider commentProvider)
        {
            this.documentationReader = documentationReader;
            this.commentProvider = commentProvider;
        }

        public int Order { get; } = 0;

        public SyntaxNode Rewrite(Assembly assemlby, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new CommentSyntaxRewriter(documentationReader, commentProvider, semanticModel);
            return rewriter.Visit(assemlby, node);
        }
    }
}