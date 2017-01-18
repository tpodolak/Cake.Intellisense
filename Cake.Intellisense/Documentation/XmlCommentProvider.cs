using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.Documentation
{
    public class XmlCommentProvider : ICommentProvider
    {
        public string Get(XDocument document, ISymbol symbol)
        {
            var comment = GetComment(document, symbol.GetDocumentationCommentId());

            if (comment == null)
                return string.Empty;

            var allNodes = comment.Elements();
            if (symbol.Kind == SymbolKind.Method)
            {
                var methodSymbol = (IMethodSymbol)symbol;
                if (methodSymbol.GetAttributes().Any(val => val.ToString().EndsWith(CakeAttributes.CakePropertyAlias) || val.ToString().EndsWith(CakeAttributes.CakeMethodAlias)))
                {
                    allNodes =
                        allNodes.Where(
                            val =>
                                val.Name != XName.Get("param") ||
                                val.Attribute(XName.Get("name"))?.Value !=
                                methodSymbol.Parameters.FirstOrDefault()?.Name).ToList();
                }
            }

            var result = string.Join(Environment.NewLine, allNodes.Select(val => val.ToString()));

            var res2 = string.Join(Environment.NewLine, result.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(val => @"/// " + val));
            return res2;

        }

        private static XElement GetComment(XDocument documentation, string commentId)
        {
            var commentSection = documentation.XPathSelectElement("//member[starts-with(@name, '" + commentId + "')]");
            return commentSection;
        }
    }
}