using System.Reflection;
using Autofac;
using Cake.MetadataGenerator.Infrastructure;
using NLog;

namespace Cake.MetadataGenerator
{
    public class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new MetadataGeneratorModule(Assembly.GetExecutingAssembly()));
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var metadataGenerator = scope.Resolve<IMetadataGenerator>();
                metadataGenerator.Generate(args);
            }
        }
    }
}
