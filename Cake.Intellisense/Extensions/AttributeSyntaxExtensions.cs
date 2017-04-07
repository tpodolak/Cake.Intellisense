using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.Intellisense.Extensions
{
    public static class AttributeSyntaxExtensions
    {
        public static string GetSimpleName(this AttributeSyntax attribute)
        {
            return GetSimpleNameFromNode(attribute)
                    .Identifier
                    .Text;
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