using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.MethodRewriters
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
                .WithBody(SyntaxFactory.Block(bodyStatements))
                .WithModifiers(modifiers)
                .WithoutTrailingTrivia()
                .WithSemicolonToken(
                    SyntaxFactory.MissingToken(SyntaxKind.SemicolonToken)
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
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.StaticKeyword)
            };

            var syntaxTokenList = SyntaxFactory.TokenList(modifierTokens);
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
                var outAssignments = outParams.Select(val => SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    SyntaxFactory.IdentifierName(val.Identifier),
                    SyntaxFactory.DefaultExpression(val.Type))));

                bodyStatements.AddRange(outAssignments);
            }

            if ((node.ReturnType as PredefinedTypeSyntax)?.Keyword.Kind() != SyntaxKind.VoidKeyword)
            {
                bodyStatements.Add(SyntaxFactory.ReturnStatement(SyntaxFactory.DefaultExpression(node.ReturnType)));
            }

            return bodyStatements;
        }

        private PropertyDeclarationSyntax GetPropertyDeclaration(MethodDeclarationSyntax node)
        {
            return SyntaxFactory.PropertyDeclaration(node.ReturnType, node.Identifier)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword))
                .AddAttributeLists(node.AttributeLists.ToArray())
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
        }

        private bool IsProperty(MethodDeclarationSyntax node)
        {
            return node.AttributeLists.Any(list => list.Attributes.Any(attr => AttributeNameMatches(attr, CakeAttributeNames.CakePropertyAlias)));
        }

        private ParameterListSyntax GetParameterList(MethodDeclarationSyntax node)
        {
            var hasCakeMethodAliasAttribute =
                node.AttributeLists.Any(
                    attributeLists =>
                        attributeLists.Attributes.Any(attr => AttributeNameMatches(attr, CakeAttributeNames.CakeMethodAlias)));

            if (!hasCakeMethodAliasAttribute || !node.ParameterList.Parameters.Any())
                return node.ParameterList;

            return SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(node.ParameterList.Parameters.Skip(hasCakeMethodAliasAttribute ? 1 : 0)));
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
