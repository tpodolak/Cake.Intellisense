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

        public XDocument Read(string documentationFile)
        {
            var fileContent = fileSystem.FileExists(documentationFile)
                ? fileSystem.ReadAllText(documentationFile)
                : "<?xml version=\"1.0\"?>";

            return XDocument.Parse(fileContent);
        }
    }
}