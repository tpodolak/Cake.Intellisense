namespace Cake.Intellisense
{
    public interface IMetadataGenerator
    {
        GeneratorResult Generate(MetadataGeneratorOptions options);
    }
}