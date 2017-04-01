using Cake.Intellisense.Infrastructure.Interfaces;

namespace Cake.Intellisense.Infrastructure
{
    public class Environment : IEnvironment
    {
        public void Exit(int exitCode)
        {
            System.Environment.Exit(exitCode);
        }
    }
}