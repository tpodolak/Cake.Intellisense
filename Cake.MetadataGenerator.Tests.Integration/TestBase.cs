using Autofac;
using Cake.MetadataGenerator.Infrastructure;

namespace Cake.MetadataGenerator.Tests.Integration
{
    public abstract class TestBase
    {
        protected IMetadataGenerator MetadataGenerator { get; }

        protected TestBase()
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
