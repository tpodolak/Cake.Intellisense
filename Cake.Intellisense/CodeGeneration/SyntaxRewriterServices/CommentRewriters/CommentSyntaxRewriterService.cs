using System;
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

        public int Order { get; } = 0;

        public CommentSyntaxRewriterService(IDocumentationReader documentationReader, ICommentProvider commentProvider)
        {
            _documentationReader = documentationReader ?? throw new ArgumentNullException(nameof(documentationReader));
            _commentProvider = commentProvider ?? throw new ArgumentNullException(nameof(commentProvider));
        }

        public SyntaxNode Rewrite(Assembly assembly, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new CommentSyntaxRewriter(_documentationReader, _commentProvider, semanticModel);
            return rewriter.Visit(assembly, node);
        }
    }
}