using Microsoft.CodeAnalysis.Host;

namespace Cake.MetadataGenerator.CodeGeneration
{
    public class RoslynCSharpCodeGenerationService : ICSharpCodeGenerationService
    {
        private readonly ILanguageService _languageService;

        public RoslynCSharpCodeGenerationService(ILanguageService languageService)
        {
            _languageService = languageService;
        }
    }
}