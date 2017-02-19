using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Cake.MetadataGenerator.Documentation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.CommentRewriters
{
    internal class CommentSyntaxRewriter
    {
        private readonly IDocumentationReader documentationReader;
        private readonly ICommentProvider provider;
        private readonly SemanticModel semanticModel;

        public CommentSyntaxRewriter(IDocumentationReader documentationReader, ICommentProvider provider, SemanticModel semanticModel)
        {
            this.documentationReader = documentationReader;
            this.provider = provider;
            this.semanticModel = semanticModel;
        }

        public SyntaxNode Visit(Assembly assembly, SyntaxNode rootNode)
        {
            var nodesDict = new Dictionary<CSharpSyntaxNode, CSharpSyntaxNode>();
            var xml = documentationReader.Read(Path.ChangeExtension(assembly.Location, "xml"));

            foreach (var node in rootNode.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                var currentNode = node;
                var declaredSymbol = semanticModel.GetDeclaredSymbol(node);
                var attributeList = node.AttributeLists;
                var commentTrivia = TriviaList(Comment(provider.Get(xml, declaredSymbol)), CarriageReturn, LineFeed);

                if (node.AttributeLists.Any())
                {
                    var attributeListSyntax = node.AttributeLists.First();
                    attributeList = attributeList.Replace(attributeListSyntax, attributeListSyntax.WithLeadingTrivia(commentTrivia));
                    currentNode = node.WithAttributeLists(attributeList);
                }
                else
                    currentNode = node.WithLeadingTrivia(commentTrivia);

                nodesDict.Add(node, currentNode);
            }

            return rootNode.ReplaceNodes(nodesDict.Keys, (originalNode, declarationSyntax) => nodesDict[originalNode]);
        }
    }
}