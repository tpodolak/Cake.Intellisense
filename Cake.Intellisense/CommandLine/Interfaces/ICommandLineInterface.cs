namespace Cake.Intellisense.CommandLine.Interfaces
{
    public interface ICommandLineInterface
    {
        MetadataGeneratorOptions Interact(string[] args);
    }
}