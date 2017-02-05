using System.IO;

namespace Cake.MetadataGenerator.FileSystem
{
    public class PhysicalFileSystem : IFileSystem
    {
        public bool DirectoryExists(string directory)
        {
            return Directory.Exists(directory);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }
    }
}