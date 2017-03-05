using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.CodeGeneration.SourceGenerators
{
    public interface ICakeSourceGeneratorService
    {
        CompilationUnitSyntax Generate(Assembly assembly);
    }
}