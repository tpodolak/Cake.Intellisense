using System.Reflection;

namespace Cake.Intellisense.Compilation
{
    public interface ICompiler
    {
        Assembly Compile(Microsoft.CodeAnalysis.Compilation compilation, string outputPath);
    }
}