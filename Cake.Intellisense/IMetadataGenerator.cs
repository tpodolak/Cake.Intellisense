namespace Cake.MetadataGenerator
{
    public interface IMetadataGenerator
    {
        void Generate(string[] args);

        GeneratorResult Generate(MetadataGeneratorOptions options);
    }
}