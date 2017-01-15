using Autofac;
using Cake.MetadataGenerator.Infrastructure;

namespace Cake.MetadataGenerator.Tests.Integration
{
    public class MetadataGeneratorFixture
    {
        public IMetadataGenerator MetadataGenerator { get; }

        public MetadataGeneratorFixture()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new MetadataGeneratorModule(typeof(IMetadataGenerator).Assembly));

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                MetadataGenerator = scope.Resolve<IMetadataGenerator>();
            }
        }
    }
}