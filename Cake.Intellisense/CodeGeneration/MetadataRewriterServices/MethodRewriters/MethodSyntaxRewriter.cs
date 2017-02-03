using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.MethodRewriters
{
    internal class MethodSyntaxRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel semanticModel;

        public MethodSyntaxRewriter(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var modifiers = GetModifiers();
            var bodyStatements = GetBodyStatements(node);
            var parameterListSyntax = GetParameterList(node);

            node = node.WithParameterList(parameterListSyntax)
                .WithBody(Block(bodyStatements))
                .WithModifiers(modifiers)
                .WithoutTrailingTrivia()
                .WithSemicolonToken(
                    MissingToken(SyntaxKind.SemicolonToken)
                        .WithLeadingTrivia(node.SemicolonToken.LeadingTrivia)
                        .WithTrailingTrivia(node.SemicolonToken.TrailingTrivia));

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
                .Where(val => semanticModel.GetDeclaredSymbol(val).RefKind == RefKind.Out)
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
                .AddAttributeLists(node.AttributeLists.ToArray())
                .AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
        }

        private bool IsProperty(MethodDeclarationSyntax node)
        {
            return node.AttributeLists.Any(list => list.Attributes.Any(attr => AttributeNameMatches(attr, CakeAttributes.CakePropertyAlias)));
        }

        private ParameterListSyntax GetParameterList(MethodDeclarationSyntax node)
        {
            var hasCakeMethodAliasAttribute =
                node.AttributeLists.Any(
                    attrs =>
                        attrs.Attributes.Any(attr => AttributeNameMatches(attr, CakeAttributes.CakeMethodAlias)));

            if (!hasCakeMethodAliasAttribute || !node.ParameterList.Parameters.Any())
                return node.ParameterList;

            return ParameterList(SeparatedList(node.ParameterList.Parameters.Skip(hasCakeMethodAliasAttribute ? 1 : 0)));
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
