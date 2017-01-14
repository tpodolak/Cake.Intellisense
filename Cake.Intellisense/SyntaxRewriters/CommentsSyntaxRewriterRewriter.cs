using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Cake.MetadataGenerator.Documentation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public class CommentsSyntaxRewriterRewriter
    {
        private readonly IDocumentationReader _documentationReader;
        private readonly ICommentProvider _provider;
        private readonly SemanticModel _semanticModel;

        public CommentsSyntaxRewriterRewriter(IDocumentationReader documentationReader, ICommentProvider provider, SemanticModel semanticModel)
        {
            _documentationReader = documentationReader;
            _provider = provider;
            _semanticModel = semanticModel;
        }

        public SyntaxNode Visit(Assembly assembly, SyntaxNode rootNode)
        {
            var xml = _documentationReader.Read(Path.ChangeExtension(assembly.Location, "xml"));
            var replace = new Dictionary<MethodDeclarationSyntax, MethodDeclarationSyntax>();
            foreach (var node in rootNode.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                var currentNode = node;
                var declaredSymbol = _semanticModel.GetDeclaredSymbol(node);
                var attributeList = node.AttributeLists;
                var commentTrivia = TriviaList(Comment(_provider.Get(xml, declaredSymbol)), CarriageReturn);

                if (node.AttributeLists.Any())
                {
                    var attributeListSyntax = node.AttributeLists.First();
                    attributeList = attributeList.Replace(attributeListSyntax, attributeListSyntax.WithLeadingTrivia(commentTrivia));
                    currentNode = node.WithAttributeLists(attributeList);
                }
                else
                {
                    currentNode = node.WithLeadingTrivia(commentTrivia);
                }

                replace.Add(node, currentNode);
            }

            return rootNode.ReplaceNodes(replace.Keys, (syntax, declarationSyntax) => replace[syntax]);
        }
    }
}