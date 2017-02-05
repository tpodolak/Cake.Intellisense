using System.Xml.Linq;
using Cake.MetadataGenerator.FileSystem;

namespace Cake.MetadataGenerator.Documentation
{
    public class DocumentationReader : IDocumentationReader
    {
        private readonly IFileSystem fileSystem;

        public DocumentationReader(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public XDocument Read(string id)
        {
            return fileSystem.DirectoryExists(id) ? XDocument.Parse(id) : XDocument.Parse("<xml> </xml>");
        }
    }
}