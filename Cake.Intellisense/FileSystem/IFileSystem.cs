using System.IO;

namespace Cake.MetadataGenerator.FileSystem
{
    public interface IFileSystem
    {
        bool DirectoryExists(string directory);

        bool FileExists(string path);

        DirectoryInfo CreateDirectory(string path);
    }
}