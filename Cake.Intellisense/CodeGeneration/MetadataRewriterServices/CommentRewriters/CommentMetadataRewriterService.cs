using System.Reflection;
using Cake.MetadataGenerator.Documentation;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.CommentRewriters
{
    public class CommentMetadataRewriterService : IMetadataRewriterService
    {
        private readonly IDocumentationReader _documentationReader;
        private readonly ICommentProvider _commentProvider;

        public CommentMetadataRewriterService(IDocumentationReader documentationReader, ICommentProvider commentProvider)
        {
            _documentationReader = documentationReader;
            _commentProvider = commentProvider;
        }

        public int Order { get; } = 0;

        public SyntaxNode Rewrite(Assembly assemlby, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new CommentSyntaxRewriter(_documentationReader, _commentProvider, semanticModel);
            return rewriter.Visit(assemlby, node);
        }
    }
}