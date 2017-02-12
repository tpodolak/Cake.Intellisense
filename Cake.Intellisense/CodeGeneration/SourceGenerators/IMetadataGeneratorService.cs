using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.CodeGeneration.SourceGenerators
{
    public interface IMetadataGeneratorService
    {
        ClassDeclarationSyntax CreateNamedTypeDeclaration(INamedTypeSymbol namedTypeSymbol);
    }
}
