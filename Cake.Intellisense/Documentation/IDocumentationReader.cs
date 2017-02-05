using System.Xml.Linq;

namespace Cake.MetadataGenerator.Documentation
{
    public interface IDocumentationReader
    {
        XDocument Read(string documentationFile);
    }
}