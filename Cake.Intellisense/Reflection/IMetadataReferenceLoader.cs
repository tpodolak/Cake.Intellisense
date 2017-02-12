using System.IO;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.Reflection
{
    public interface IMetadataReferenceLoader
    {
        PortableExecutableReference CreateFromFile(string path);

        PortableExecutableReference CreateFromStream(Stream stream);
    }
}