using Microsoft.CodeAnalysis.Host;

namespace Cake.MetadataGenerator.CodeGeneration.MetadataGenerators
{
    public interface ILanguageServiceProvider
    {
        ILanguageService Get();
    }
}