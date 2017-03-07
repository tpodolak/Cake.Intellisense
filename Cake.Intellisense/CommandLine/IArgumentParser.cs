namespace Cake.Intellisense.CommandLine
{
    public interface IArgumentParser
    {
        ParserResult<T> Parse<T>(string[] arguments) where T : class, new();
    }
}