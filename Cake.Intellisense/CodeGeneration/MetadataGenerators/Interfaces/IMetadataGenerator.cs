using Cake.Intellisense.CommandLine;

namespace Cake.Intellisense.CodeGeneration.MetadataGenerators.Interfaces
{
    public interface IMetadataGenerator
    {
        GeneratorResult Generate(MetadataGeneratorOptions options);
    }
}