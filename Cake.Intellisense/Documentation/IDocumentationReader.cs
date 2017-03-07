using System.Xml.Linq;

namespace Cake.Intellisense.Documentation
{
    public interface IDocumentationReader
    {
        XDocument Read(string documentationFile);
    }
}