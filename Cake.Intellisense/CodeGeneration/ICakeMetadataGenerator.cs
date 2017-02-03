using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration
{
    public interface ICakeMetadataGenerator
    {
        SyntaxTree Generate(Assembly assembly);
    }
}