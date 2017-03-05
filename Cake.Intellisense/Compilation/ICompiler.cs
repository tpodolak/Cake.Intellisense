using System.Reflection;

namespace Cake.MetadataGenerator.Compilation
{
    public interface ICompiler
    {
        Assembly Compile(Microsoft.CodeAnalysis.Compilation compilation, string outputPath);
    }
}