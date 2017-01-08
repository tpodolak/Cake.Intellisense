using NLog;

namespace Cake.MetadataGenerator
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var generator = new MetadataGenerator();
            generator.Generate(args);
        }
    }
}
