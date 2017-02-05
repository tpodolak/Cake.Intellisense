namespace Cake.MetadataGenerator
{
    public interface IMetadataGenerator
    {
        GeneratorResult Generate(MetadataGeneratorOptions options);
    }
}