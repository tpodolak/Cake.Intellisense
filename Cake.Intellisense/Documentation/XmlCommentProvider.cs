using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Cake.Intellisense.Documentation.Interfaces;
using Microsoft.CodeAnalysis;
using MoreLinq;

namespace Cake.Intellisense.Documentation
{
    public class XmlCommentProvider : ICommentProvider
    {
        public string Get(XDocument document, ISymbol symbol)
        {
            var comment = GetCommentElement(document, symbol.GetDocumentationCommentId());

            if (comment == null)
                return string.Empty;

            comment = PreprocessCommentElement(symbol, comment);

            return ConvertToString(comment);
        }

        private XElement PreprocessCommentElement(ISymbol symbol, XElement comment)
        {
            var methodSymbol = symbol as IMethodSymbol;
            if (symbol.Kind == SymbolKind.Method && HasCakeAttributes(methodSymbol))
            {
                var remove = comment.Elements().Where(val => val.Attribute(XName.Get("name"))?.Value == methodSymbol.Parameters.FirstOrDefault()?.Name);
                remove.ForEach(val => val.Remove());
            }

            return comment;
        }

        private bool HasCakeAttributes(IMethodSymbol methodSymbol)
        {
            return methodSymbol.GetAttributes()
                .Any(
                    val =>
                        val.AttributeClass.Name.EndsWith(CakeAttributeNames.CakePropertyAlias) ||
                        val.AttributeClass.Name.EndsWith(CakeAttributeNames.CakeMethodAlias));
        }

        private XElement GetCommentElement(XDocument documentation, string commentId)
        {
            var commentSection = documentation.XPathSelectElement("//member[starts-with(@name, '" + commentId + "')]");
            return commentSection;
        }

        private string ConvertToString(XElement comment)
        {
            using (var reader = comment.CreateReader())
            {
                var charArray = Environment.NewLine.ToCharArray();
                reader.MoveToContent();
                var readInnerXml = reader.ReadInnerXml();
                return string.Join(Environment.NewLine, readInnerXml.Split(charArray, StringSplitOptions.RemoveEmptyEntries).Select(val => @"/// " + val));
            }
        }
    }
}