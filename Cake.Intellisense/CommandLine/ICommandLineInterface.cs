namespace Cake.MetadataGenerator.CommandLine
{
    public interface ICommandLineInterface
    {
        MetadataGeneratorOptions Interact(string[] args);
    }
}