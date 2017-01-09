using Microsoft.CodeAnalysis.Host;

namespace Cake.MetadataGenerator.CodeGeneration
{
    public interface ICSharpCodeGenerationServiceProvider
    {
        ILanguageService Get();
    }
}