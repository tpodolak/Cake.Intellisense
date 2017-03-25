namespace Cake.Intellisense.CommandLine.Interfaces
{
    public interface IArgumentParser
    {
        ParserResult<T> Parse<T>(string[] arguments) where T : class, new();
    }
}