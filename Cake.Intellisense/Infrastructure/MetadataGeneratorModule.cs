using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Cake.MetadataGenerator.Infrastructure
{
    public class MetadataGeneratorModule : Module
    {
        private readonly Assembly _assembly;

        public MetadataGeneratorModule(Assembly assembly)
        {
            _assembly = assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assembly)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}