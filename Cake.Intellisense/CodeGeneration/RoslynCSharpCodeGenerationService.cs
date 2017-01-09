using System.Reflection;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Host;

namespace Cake.MetadataGenerator.CodeGeneration
{
    public class RoslynCSharpCodeGenerationService : ICSharpCodeGenerationService
    {
        private readonly ILanguageService _languageService;

        private readonly MethodInfo _namedTypeDeclarationMethod;

        public RoslynCSharpCodeGenerationService(ILanguageService languageService)
        {
            _languageService = languageService;
            _namedTypeDeclarationMethod = languageService.GetType().GetMethod("CreateNamedTypeDeclaration");
        }

        public ClassDeclarationSyntax CreateNamedTypeDeclaration(INamedTypeSymbol namedTypeSymbol)
        {
            return (ClassDeclarationSyntax)_namedTypeDeclarationMethod.Invoke(_languageService, new object[] { namedTypeSymbol, 0, null, CancellationToken.None });
        }
    }
}