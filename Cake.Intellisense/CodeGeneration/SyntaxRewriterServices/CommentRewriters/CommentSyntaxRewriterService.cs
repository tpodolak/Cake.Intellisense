using System.Reflection;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.Interfaces;
using Cake.Intellisense.Documentation;
using Cake.Intellisense.Documentation.Interfaces;
using Microsoft.CodeAnalysis;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CommentRewriters
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

        public SyntaxNode Rewrite(Assembly assembly, SemanticModel semanticModel, SyntaxNode node)
        {
            var rewriter = new CommentSyntaxRewriter(documentationReader, commentProvider, semanticModel);
            return rewriter.Visit(assembly, node);
        }
    }
}