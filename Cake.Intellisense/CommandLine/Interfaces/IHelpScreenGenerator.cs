namespace Cake.Intellisense.CommandLine.Interfaces
{
    public interface IHelpScreenGenerator
    {
        string Generate<T>() where T : class, new();
    }
}