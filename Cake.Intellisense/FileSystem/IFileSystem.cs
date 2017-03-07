using System.IO;

namespace Cake.Intellisense.FileSystem
{
    public interface IFileSystem
    {
        bool FileExists(string path);

        string ReadAllText(string path);

        bool DirectoryExists(string directory);

        DirectoryInfo CreateDirectory(string path);
    }
}