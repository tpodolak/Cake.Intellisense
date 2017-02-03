using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Cake.MetadataGenerator.Infrastructure
{
    public class MetadataGeneratorModule : Module
    {
        private readonly Assembly assembly;

        public MetadataGeneratorModule(Assembly assembly)
        {
            this.assembly = assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}