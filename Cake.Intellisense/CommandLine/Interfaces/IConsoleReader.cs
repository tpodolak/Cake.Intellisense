namespace Cake.Intellisense.CommandLine.Interfaces
{
    public interface IConsoleReader
    {
        string Read();

        bool TryRead<T>(out T result);
    }
}