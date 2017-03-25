using System.Reflection;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.Interfaces;
using Cake.Intellisense.Documentation.Interfaces;
using Microsoft.CodeAnalysis;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CommentRewriters
{
    public class CommentSyntaxRewriterService : ISyntaxRewriterService
    {
        private readonly IDocumentationReader _documentationReader;
        private readonly ICommentProvider _commentProvider;

        public CommentSyntaxRewriterService(IDocumentationReader documentationReader, ICommentProvider commentProvider)
        {
            _documentationReader = documentationReader;
            _commentProvider = commentProvider;
        }

        public int Order { get; } = 0;

        public SyntaxNode Rewrite(Assembly assembly, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new CommentSyntaxRewriter(_documentationReader, _commentProvider, semanticModel);
            return rewriter.Visit(assembly, node);
        }
    }
}