using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public class CakeClassSyntaxRewriter : CSharpSyntaxRewriter
    {
        private static readonly List<SyntaxKind> tabuModifiers = new List<SyntaxKind>
        {
            SyntaxKind.ProtectedKeyword,
            SyntaxKind.PrivateKeyword,
            SyntaxKind.InternalKeyword
        };

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node.Modifiers.All(val => val.Kind() != SyntaxKind.PublicKeyword))
                return null;

//            var newAttributes = RemoveAttributes(node.AttributeLists, "CakeAliasCategoryAttribute");
//            var leadTriv = node.GetLeadingTrivia();
//            node = node.WithAttributeLists(newAttributes)
//                .WithLeadingTrivia(leadTriv);
            return base.VisitClassDeclaration(node.WithIdentifier(SyntaxFactory.Identifier(node.Identifier.Text + "Metadata")));
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.Modifiers.Any(modifier => tabuModifiers.Any(tabu => tabu == modifier.Kind())))
            {
                return null;
            }

//            var newAttributes = RemoveAttributes(node.AttributeLists, "CakeMethodAliasAttribute");
//            var leadTriv = node.GetLeadingTrivia();
//            node = node.WithAttributeLists(newAttributes)
//                .WithLeadingTrivia(leadTriv);

            return base.VisitMethodDeclaration(node);
        }

        private SyntaxList<AttributeListSyntax> RemoveAttributes(SyntaxList<AttributeListSyntax> originalAttributes, string attributeToRemove)
        {
            var newAttributes = new SyntaxList<AttributeListSyntax>();

            foreach (var attributeList in originalAttributes)
            {
                var nodesToRemove = attributeList.Attributes.Where(attribute => AttributeNameMatches(attribute, attributeToRemove)).ToArray();
                var syntax = attributeList.RemoveNodes(nodesToRemove,
                    SyntaxRemoveOptions.KeepExteriorTrivia | SyntaxRemoveOptions.KeepLeadingTrivia |
                    SyntaxRemoveOptions.KeepTrailingTrivia);
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
