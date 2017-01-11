﻿using System.Xml.Linq;

namespace Cake.MetadataGenerator.Documentation
{
    public class AssemblyDocumentationReader : IDocumentationReader
    {
        public XDocument Read(string id)
        {
            return XDocument.Load(id);
        }
    }

    public interface IDocumentationReader
    {
        XDocument Read(string id);
    }
}