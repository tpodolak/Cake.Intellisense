using System.Reflection;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Host;

namespace Cake.MetadataGenerator.CodeGeneration.SourceGenerators
{
    public class RoslynMetadataGeneratorService : IMetadataGeneratorService
    {
        private readonly ILanguageService languageService;

        private readonly MethodInfo namedTypeDeclarationMethod;

        public RoslynMetadataGeneratorService()
        {
            languageService = CreateLanguageService();
            namedTypeDeclarationMethod = languageService.GetType().GetMethod("CreateNamedTypeDeclaration");
        }

        public ClassDeclarationSyntax CreateNamedTypeDeclaration(INamedTypeSymbol namedTypeSymbol)
        {
            return (ClassDeclarationSyntax)namedTypeDeclarationMethod.Invoke(languageService, new object[] { namedTypeSymbol, 0, null, CancellationToken.None });
        }

        private ILanguageService CreateLanguageService()
        {
            var project = new AdhocWorkspace().AddSolution(SolutionInfo.Create(SolutionId.CreateNewId("MetadataGenerator"), VersionStamp.Default))
                                              .AddProject("Project", "Assembly", LanguageNames.CSharp);

            var languageServices = project.LanguageServices;
            var host = nameof(languageServices.GetService);
            var service = typeof(ILanguageService).Assembly.GetType("Microsoft.CodeAnalysis.CodeGeneration.ICodeGenerationService");
            
            return (ILanguageService)languageServices.GetType().GetMethod(host).MakeGenericMethod(service).Invoke(languageServices, null);
        }
    }
}