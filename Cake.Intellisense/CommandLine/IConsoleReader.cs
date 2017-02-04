namespace Cake.MetadataGenerator.CommandLine
{
    public interface IConsoleReader
    {
        string Read();

        bool TryRead<T>(out T result);
    }
}