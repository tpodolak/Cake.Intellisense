using System.Collections.Generic;
using System.Linq;
using Cake.MetadataGenerator.Documentation;
using Cake.MetadataGenerator.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public class CakeMethodBodySyntaxRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        private readonly IDocumentationProvider _documentationProvider;

        private static readonly List<SyntaxKind> tabuModifiers = new List<SyntaxKind>
        {
            SyntaxKind.ProtectedKeyword,
            SyntaxKind.PrivateKeyword,
            SyntaxKind.InternalKeyword
        };

        public CakeMethodBodySyntaxRewriter(SemanticModel semanticModel, IDocumentationProvider documentationProvider)
        {
            _semanticModel = semanticModel;
            _documentationProvider = documentationProvider;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.Modifiers.All(val => val.Kind() != SyntaxKind.PublicKeyword))
                return base.VisitMethodDeclaration(node);

            var attributeList = node.AttributeLists;
            var bodyStatements = new List<StatementSyntax>();
            var modifierTokens = new List<SyntaxToken>
            {
                Token(SyntaxKind.PublicKeyword),
                Token(SyntaxKind.StaticKeyword)
            };

            var methodSymbol = _semanticModel.GetDeclaredSymbol(node);
            var comment = methodSymbol.GetDocumentationCommentId();

            var outParams = node.ParameterList.Parameters
                                              .Where(val => _semanticModel.GetDeclaredSymbol(val).RefKind == RefKind.Out)
                                              .ToList();

            if (outParams.Any())
            {
                var outAssignments = outParams.Select(val => ExpressionStatement(AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    IdentifierName(val.Identifier),
                    DefaultExpression(val.Type))));

                bodyStatements.AddRange(outAssignments);
            }

            if ((node.ReturnType as PredefinedTypeSyntax)?.Keyword.Kind() != SyntaxKind.VoidKeyword)
            {
                bodyStatements.Add(ReturnStatement(DefaultExpression(node.ReturnType)));
            }
            var text = _documentationProvider.Get(comment);

            var commentTrivia = TriviaList(Comment(text), CarriageReturn);


            if (node.AttributeLists.Any())
            {
                var attributeListSyntax = node.AttributeLists.First();
                attributeList = attributeList.Replace(attributeListSyntax, attributeListSyntax.WithLeadingTrivia(TriviaList(Comment(text))));
            }

            node = node.WithLeadingTrivia(commentTrivia).WithParameterList(GetParameterList(node))
                       .WithBody(Block(bodyStatements))
                       .WithAttributeLists(attributeList)
                       .WithModifiers(TokenList(modifierTokens));


            if (node.AttributeLists.Any(list => list.Attributes.Any(attr => AttributeNameMatches(attr, "CakePropertyAliasAttribute"))))
            {
                return PropertyDeclaration(node.ReturnType, node.Identifier).WithLeadingTrivia(commentTrivia)
                    .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword))
                    .AddAccessorListAccessors(
                        AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
            }

            return base.VisitMethodDeclaration(node);
        }

        private ParameterListSyntax GetParameterList(MethodDeclarationSyntax node)
        {
            var aliasAttributes =
                node.AttributeLists.Any(
                    attrs =>
                        attrs.Attributes.Any(attr => AttributeNameMatches(attr, "CakeMethodAliasAttribute")));

            return ParameterList(SeparatedList(node.ParameterList.Parameters.Skip(aliasAttributes ? 1 : 0)));
        }
        private bool AttributeNameMatches(AttributeSyntax attribute, string attributeName)
        {
            return
                GetSimpleNameFromNode(attribute)
                .Identifier
                .Text
                .StartsWith(attributeName);
        }

        private SimpleNameSyntax GetSimpleNameFromNode(AttributeSyntax node)
        {
            var identifierNameSyntax = node.Name as IdentifierNameSyntax;
            var qualifiedNameSyntax = node.Name as QualifiedNameSyntax;

            return
                identifierNameSyntax
                ??
                qualifiedNameSyntax?.Right
                ??
                (node.Name as AliasQualifiedNameSyntax).Name;
        }
    }
}
