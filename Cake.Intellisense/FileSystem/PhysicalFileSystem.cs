using System.IO;

namespace Cake.Intellisense.FileSystem
{
    public class PhysicalFileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public bool DirectoryExists(string directory)
        {
            return Directory.Exists(directory);
        }

        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }
    }
}