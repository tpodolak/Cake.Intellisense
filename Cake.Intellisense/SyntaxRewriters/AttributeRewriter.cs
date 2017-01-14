using System;
using System.Linq;
using Cake.Core.Annotations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public class AttributeRewriter : CSharpSyntaxRewriter
    {
        private static readonly string[] AttributeToRemove =
        {
            CakeAttributes.CakeAliasCategory,
            CakeAttributes.CakeMethodAlias,
            CakeAttributes.CakeNamespaceImport,
            CakeAttributes.CakePropertyAlias
        };

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var newAttributes = RewriteAttributeList(node.AttributeLists);
            var leadTriv = node.GetLeadingTrivia();
            node = node.WithAttributeLists(newAttributes).WithLeadingTrivia(leadTriv);

            return base.VisitClassDeclaration(node);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var newAttributes = RewriteAttributeList(node.AttributeLists);
            var leadTriv = node.GetLeadingTrivia();
            node = node.WithAttributeLists(newAttributes).WithLeadingTrivia(leadTriv);

            return base.VisitMethodDeclaration(node);
        }

        public override SyntaxNode VisitAttribute(AttributeSyntax node)
        {
            if (AttributeNameMatches(node, typeof(ObsoleteAttribute).Name))
                node = node.WithArgumentList(SyntaxFactory.AttributeArgumentList());

            return base.VisitAttribute(node);
        }

        private SyntaxList<AttributeListSyntax> RewriteAttributeList(SyntaxList<AttributeListSyntax> originalAttributes)
        {
            var newAttributes = new SyntaxList<AttributeListSyntax>();

            foreach (var attributeList in originalAttributes)
            {
                var nodesToRemove = attributeList.Attributes.Where(attribute => AttributeToRemove.Any(attr => AttributeNameMatches(attribute, attr))).ToArray();
                var syntax = attributeList.RemoveNodes(nodesToRemove, SyntaxRemoveOptions.KeepExteriorTrivia | SyntaxRemoveOptions.KeepLeadingTrivia | SyntaxRemoveOptions.KeepTrailingTrivia);

                if (syntax.Attributes.Any())
                    newAttributes = newAttributes.Add(syntax);
            }

            return newAttributes;
        }

        private bool AttributeNameMatches(AttributeSyntax attribute, string attributeName)
        {
            return
                GetSimpleNameFromNode(attribute)
                .Identifier
                .Text
                .StartsWith(attributeName);
        }

        private static SimpleNameSyntax GetSimpleNameFromNode(AttributeSyntax node)
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