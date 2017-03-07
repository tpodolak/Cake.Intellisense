using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.Intellisense.CodeGeneration.SourceGenerators
{
    public interface ICakeSourceGeneratorService
    {
        CompilationUnitSyntax Generate(Assembly assembly);
    }
}