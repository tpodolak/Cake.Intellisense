using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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
            node = node.WithIdentifier(Identifier(node.Identifier.Text + "Metadata"));
            return base.VisitClassDeclaration(node);
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
