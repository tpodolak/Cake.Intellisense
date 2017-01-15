using System.Reflection;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Host;

namespace Cake.MetadataGenerator.CodeGeneration.LanguageServices
{
    public class RoslynCSharpCodeGenerationService : ICSharpCodeGenerationService
    {
        private readonly ILanguageService _languageService;

        private readonly MethodInfo _namedTypeDeclarationMethod;

        public RoslynCSharpCodeGenerationService(ICSharpCodeGenerationServiceProvider languageServiceProvider)
        {
            _languageService = languageServiceProvider.Get();
            _namedTypeDeclarationMethod = _languageService.GetType().GetMethod("CreateNamedTypeDeclaration");
        }

        public ClassDeclarationSyntax CreateNamedTypeDeclaration(INamedTypeSymbol namedTypeSymbol)
        {
            return (ClassDeclarationSyntax)_namedTypeDeclarationMethod.Invoke(_languageService, new object[] { namedTypeSymbol, 0, null, CancellationToken.None });
        }
    }
}