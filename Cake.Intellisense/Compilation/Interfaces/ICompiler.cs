using System.Reflection;

namespace Cake.Intellisense.Compilation.Interfaces
{
    public interface ICompiler
    {
        Assembly Compile(Microsoft.CodeAnalysis.Compilation compilation, string outputPath);
    }
}