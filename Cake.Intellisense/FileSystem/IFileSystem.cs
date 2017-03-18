using System.IO;

namespace Cake.Intellisense.FileSystem
{
    public interface IFileSystem
    {
        bool FileExists(string path);

        string ReadAllText(string path);

        void WriteAllBytes(string path, byte[] bytes);

        bool DirectoryExists(string directory);

        DirectoryInfo CreateDirectory(string path);
    }
}