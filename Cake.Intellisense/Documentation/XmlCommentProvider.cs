using System;
using System.Linq;
using System.Xml;
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

            if (symbol.Kind == SymbolKind.Method)
            {
                var methodSymbol = (IMethodSymbol)symbol;
                if (methodSymbol.GetAttributes().Any(val => val.AttributeClass.Name.EndsWith(CakeAttributes.CakePropertyAlias) || val.AttributeClass.Name.ToString().EndsWith(CakeAttributes.CakeMethodAlias)))
                {
                    var remove = comment.Elements().Where(val => val.Attribute(XName.Get("name"))?.Value == methodSymbol.Parameters.FirstOrDefault()?.Name).ToList();
                    remove.ForEach(val => val.Remove());
                }
            }

            using (var reader = comment.CreateReader())
            {
                reader.MoveToContent();
                var readInnerXml = reader.ReadInnerXml();
                var res2 = string.Join(Environment.NewLine, readInnerXml.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(val => @"/// " + val));
                return res2;
            }
        }

        private static XElement GetComment(XDocument documentation, string commentId)
        {
            var commentSection = documentation.XPathSelectElement("//member[starts-with(@name, '" + commentId + "')]");
            return commentSection;
        }
    }
}