using System.Xml.Linq;

namespace Cake.MetadataGenerator.Documentation
{
    public class DocumentationReader : IDocumentationReader
    {
        public XDocument Read(string id)
        {
            return XDocument.Load(id);
        }
    }
}