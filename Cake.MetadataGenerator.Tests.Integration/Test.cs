using System.Configuration;
using Autofac;
using Cake.MetadataGenerator.Infrastructure;

namespace Cake.MetadataGenerator.Tests.Integration
{
    public abstract class Test
    {
        protected IMetadataGenerator MetadataGenerator { get; }

        protected Test()
        {
            var builder = new ContainerBuilder();
            var metadataGeneratorModule = new MetadataGeneratorModule(ConfigurationManager.AppSettings, typeof(IMetadataGenerator).Assembly);
            builder.RegisterModule(metadataGeneratorModule);

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                MetadataGenerator = scope.Resolve<IMetadataGenerator>();
            }
        }
    }
}
