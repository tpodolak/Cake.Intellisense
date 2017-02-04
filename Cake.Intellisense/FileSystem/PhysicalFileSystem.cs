using System.IO;

namespace Cake.MetadataGenerator.FileSystem
{
    public class PhysicalFileSystem : IFileSystem
    {
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