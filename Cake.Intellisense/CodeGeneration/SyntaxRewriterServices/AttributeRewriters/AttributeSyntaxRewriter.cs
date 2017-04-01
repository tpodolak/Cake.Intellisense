using System;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Cake.Intellisense.Constants.CakeAttributeNames;
using static Cake.Intellisense.Constants.MetadataGeneration;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.AttributeRewriters
{
    internal class AttributeSyntaxRewriter : CSharpSyntaxRewriter
    {
        private static readonly string[] AttributesToRemove =
        {
            CakeAliasCategoryName,
            CakeMethodAliasName,
            CakeNamespaceImportName,
            CakePropertyAliasName
        };

        private readonly Assembly _assembly;

        public AttributeSyntaxRewriter(Assembly assembly)
        {
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
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
            var assemblyTitleAttributeName = typeof(AssemblyTitleAttribute).Name;
            if (AttributeNameMatches(node, obsoleteAttributeName) || AttributeNameMatches(node, obsoleteAttributeName.Substring(0, obsoleteAttributeName.IndexOf("Attribute", StringComparison.Ordinal))))
                node = node.WithArgumentList(null);

            if (AttributeNameMatches(node, assemblyTitleAttributeName.Substring(0, assemblyTitleAttributeName.IndexOf("Attribute", StringComparison.Ordinal))))
            {
                node =
                    node.WithArgumentList(AttributeArgumentList(SeparatedList(new[]
                    {
                        AttributeArgument(
                            LiteralExpression(
                                SyntaxKind.StringLiteralExpression,
                                Literal($"{_assembly.GetName().Name}.{MetadataClassSuffix}")))
                    })));
            }

            return base.VisitAttribute(node);
        }

        private SyntaxList<AttributeListSyntax> RewriteAttributeList(SyntaxList<AttributeListSyntax> originalAttributes)
        {
            var newAttributes = new SyntaxList<AttributeListSyntax>();

            foreach (var attributeList in originalAttributes)
            {
                var nodesToRemove = attributeList.Attributes.Where(attribute => AttributesToRemove.Any(attr => AttributeNameMatches(attribute, attr))).ToArray();
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