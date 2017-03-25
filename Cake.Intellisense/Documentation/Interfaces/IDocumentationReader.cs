using System.Xml.Linq;

namespace Cake.Intellisense.Documentation.Interfaces
{
    public interface IDocumentationReader
    {
        XDocument Read(string documentationFile);
    }
}