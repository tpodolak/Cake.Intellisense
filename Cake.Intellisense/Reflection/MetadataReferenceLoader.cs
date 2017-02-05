using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.Reflection
{
    public class MetadataReferenceLoader : IMetadataReferenceLoader
    {
        public PortableExecutableReference CreateFromFile(string path)
        {
            return MetadataReference.CreateFromFile(path);
        }
    }
}