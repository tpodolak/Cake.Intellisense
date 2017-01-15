using Microsoft.CodeAnalysis.Host;

namespace Cake.MetadataGenerator.CodeGeneration.LanguageServices
{
    public interface ICSharpCodeGenerationServiceProvider
    {
        ILanguageService Get();
    }
}