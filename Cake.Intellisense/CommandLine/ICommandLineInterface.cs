namespace Cake.Intellisense.CommandLine
{
    public interface ICommandLineInterface
    {
        MetadataGeneratorOptions Interact(string[] args);
    }
}