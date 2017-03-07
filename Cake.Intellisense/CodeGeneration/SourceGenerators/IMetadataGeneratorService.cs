using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.Intellisense.CodeGeneration.SourceGenerators
{
    public interface IMetadataGeneratorService
    {
        ClassDeclarationSyntax CreateNamedTypeDeclaration(INamedTypeSymbol namedTypeSymbol);
    }
}
