using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.Documentation
{
    public interface ICommentProvider
    {
        string Get(XDocument document, ISymbol symbol);
    }
}