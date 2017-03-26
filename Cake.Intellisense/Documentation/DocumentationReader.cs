using System;
using System.Xml.Linq;
using Cake.Intellisense.Documentation.Interfaces;
using Cake.Intellisense.FileSystem.Interfaces;

namespace Cake.Intellisense.Documentation
{
    public class DocumentationReader : IDocumentationReader
    {
        private readonly IFileSystem _fileSystem;

        public DocumentationReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public XDocument Read(string documentationFile)
        {
            var fileContent = _fileSystem.FileExists(documentationFile)
                ? _fileSystem.ReadAllText(documentationFile)
                : "<?xml version=\"1.0\"?><doc></doc>";

            return XDocument.Parse(fileContent, LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo);
        }
    }
}