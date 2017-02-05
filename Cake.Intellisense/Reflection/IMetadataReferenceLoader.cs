using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.Reflection
{
    public interface IMetadataReferenceLoader
    {
        PortableExecutableReference CreateFromFile(string path);
    }
}