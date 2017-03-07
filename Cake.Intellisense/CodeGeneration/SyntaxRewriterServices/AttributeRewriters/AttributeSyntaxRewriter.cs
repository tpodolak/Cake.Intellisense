using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.AttributeRewriters
{
    internal class AttributeSyntaxRewriter : CSharpSyntaxRewriter
    {
        private readonly string[] attributesToRemove;

        public AttributeSyntaxRewriter(string[] attributesToRemove)
        {
            this.attributesToRemove = attributesToRemove;
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var newAttributes = RewriteAttributeList(node.AttributeLists);
            var leadTriv = node.GetLeadingTrivia();
            node = node.WithAttributeLists(newAttributes).WithLeadingTrivia(leadTriv);

            return base.VisitPropertyDeclaration(node);
        }

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
            var obsoleteAttributeName = typeof(ObsoleteAttribute).Name;
            if (AttributeNameMatches(node, obsoleteAttributeName) || AttributeNameMatches(node, obsoleteAttributeName.Substring(0, obsoleteAttributeName.IndexOf("Attribute"))))
                node = node.WithArgumentList(null);

            return base.VisitAttribute(node);
        }

        private SyntaxList<AttributeListSyntax> RewriteAttributeList(SyntaxList<AttributeListSyntax> originalAttributes)
        {
            var newAttributes = new SyntaxList<AttributeListSyntax>();

            foreach (var attributeList in originalAttributes)
            {
                var nodesToRemove = attributeList.Attributes.Where(attribute => attributesToRemove.Any(attr => AttributeNameMatches(attribute, attr))).ToArray();
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