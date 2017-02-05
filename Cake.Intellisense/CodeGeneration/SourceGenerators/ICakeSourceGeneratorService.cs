using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.CodeGeneration
{
    public interface ICakeSourceGeneratorService
    {
        CompilationUnitSyntax Generate(Assembly assembly);
    }
}