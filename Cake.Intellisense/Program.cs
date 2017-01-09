using System.Linq;
using System.Reflection;
using Cake.MetadataGenerator.CodeGeneration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Host;
using NLog;

namespace Cake.MetadataGenerator
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var service = new CSharpCodeGenerationServiceProvider().Get();
            var generator = new MetadataGenerator(new RoslynCSharpCodeGenerationService(service));
            generator.Generate(args);
        }
    }
}
