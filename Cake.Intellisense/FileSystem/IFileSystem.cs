using System.IO;

namespace Cake.MetadataGenerator.FileSystem
{
    public interface IFileSystem
    {
        bool DirectoryExists(string directory);
        DirectoryInfo CreateDirectory(string path);
    }
}