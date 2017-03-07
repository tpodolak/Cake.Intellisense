using System.IO;
using Microsoft.CodeAnalysis;

namespace Cake.Intellisense.Reflection
{
    public interface IMetadataReferenceLoader
    {
        PortableExecutableReference CreateFromFile(string path);

        PortableExecutableReference CreateFromStream(Stream stream);
    }
}