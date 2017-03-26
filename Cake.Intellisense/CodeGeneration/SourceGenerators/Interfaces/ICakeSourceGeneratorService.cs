using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.Intellisense.CodeGeneration.SourceGenerators.Interfaces
{
    public interface ICakeSourceGeneratorService
    {
        CompilationUnitSyntax Generate(Assembly assembly);
    }
}