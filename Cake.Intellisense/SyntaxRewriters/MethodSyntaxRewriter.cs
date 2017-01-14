using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public class MethodSyntaxRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;

        public MethodSyntaxRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var modifiers = GetModifiers();
            var bodyStatements = GetBodyStatements(node);
            var parameterListSyntax = GetParameterList(node);

            node = node.WithParameterList(parameterListSyntax)
                       .WithBody(Block(bodyStatements))
                       .WithModifiers(modifiers);


            if (IsProperty(node))
                return GetPropertyDeclaration(node);

            return base.VisitMethodDeclaration(node);
        }

        private SyntaxTokenList GetModifiers()
        {
            var modifierTokens = new List<SyntaxToken>
            {
                Token(SyntaxKind.PublicKeyword),
                Token(SyntaxKind.StaticKeyword)
            };

            var syntaxTokenList = TokenList(modifierTokens);
            return syntaxTokenList;
        }

        private IEnumerable<StatementSyntax> GetBodyStatements(MethodDeclarationSyntax node)
        {
            var bodyStatements = new List<StatementSyntax>();

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

            return bodyStatements;
        }

        private PropertyDeclarationSyntax GetPropertyDeclaration(MethodDeclarationSyntax node)
        {
            return PropertyDeclaration(node.ReturnType, node.Identifier)
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword))
                .AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
        }

        private bool IsProperty(MethodDeclarationSyntax node)
        {
            return node.AttributeLists.Any(list => list.Attributes.Any(attr => AttributeNameMatches(attr, CakeAttributes.CakePropertyAlias)));
        }

        private ParameterListSyntax GetParameterList(MethodDeclarationSyntax node)
        {
            var aliasAttributes =
                node.AttributeLists.Any(
                    attrs =>
                        attrs.Attributes.Any(attr => AttributeNameMatches(attr, CakeAttributes.CakeMethodAlias)));

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
