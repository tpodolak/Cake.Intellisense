using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public class CakeAttributesRemoverSyntaxRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;

        public CakeAttributesRemoverSyntaxRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var newAttributes = RemoveAttributes(node.AttributeLists, "CakeAliasCategoryAttribute");
            var leadTriv = node.GetLeadingTrivia();
            node = node.WithAttributeLists(newAttributes)
                .WithLeadingTrivia(leadTriv);
            return base.VisitClassDeclaration(node);
        }
        
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.AttributeLists.Any(list => list.Attributes.Any(attr => AttributeNameMatches(attr, "CakeMethodAliasAttribute"))))
            {
                var newParams = RemoveCakeContextParameter(node.ParameterList);
                node = node.WithParameterList(newParams);
            }

            var newAttributes = RemoveAttributes(node.AttributeLists, "CakeMethodAliasAttribute");
            var leadTriv = node.GetLeadingTrivia();
            node = node.WithAttributeLists(newAttributes)
                .WithLeadingTrivia(leadTriv);

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

        private ParameterListSyntax RemoveCakeContextParameter(ParameterListSyntax originalParameters)
        {
            ParameterListSyntax parameterListSyntax;
            if (originalParameters.Parameters.Count > 0)
                parameterListSyntax = SyntaxFactory.ParameterList(originalParameters.Parameters.RemoveAt(0));
            else
                parameterListSyntax = SyntaxFactory.ParameterList();

            return parameterListSyntax;
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
