namespace Cake.Intellisense.CommandLine
{
    public interface IConsoleReader
    {
        string Read();

        bool TryRead<T>(out T result);
    }
}