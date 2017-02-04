using System.Configuration;
using System.Reflection;
using Autofac;
using Cake.MetadataGenerator.Infrastructure;

namespace Cake.MetadataGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var module = new MetadataGeneratorModule(ConfigurationManager.AppSettings, Assembly.GetExecutingAssembly());
            builder.RegisterModule(module);
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var metadataGenerator = scope.Resolve<IMetadataGenerator>();
                metadataGenerator.Generate(args);
            }
        }
    }
}
