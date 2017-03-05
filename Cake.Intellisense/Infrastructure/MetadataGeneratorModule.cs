using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Autofac;
using Cake.MetadataGenerator.Settings;
using Castle.Components.DictionaryAdapter;
using Module = Autofac.Module;

namespace Cake.MetadataGenerator.Infrastructure
{
    public class MetadataGeneratorModule : Module
    {
        private readonly NameValueCollection appSettings;
        private readonly Assembly assembly;

        public MetadataGeneratorModule(NameValueCollection appSettings, Assembly assembly)
        {
            this.appSettings = appSettings;
            this.assembly = assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var referencedAssemblies = assembly.GetReferencedAssemblies().Select(Assembly.Load);

            builder.RegisterAssemblyTypes(referencedAssemblies.Union(new[] { assembly }).ToArray())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            RegisterSettings(builder);
        }

        private void RegisterSettings(ContainerBuilder builder)
        {
            var factory = new DictionaryAdapterFactory();
            var appSettingsAdapter = new NameValueCollectionAdapter(appSettings);
            var descriptor = new PropertyDescriptor().AddBehavior(new SettingsBehavior());
            var settingsType = typeof(ISettings);

            foreach (var type in assembly.ExportedTypes.Where(val => val.IsInterface && val.IsAssignableTo<ISettings>() && val != settingsType))
            {
                builder.RegisterInstance(factory.GetAdapter(type, appSettingsAdapter, descriptor)).As(type);
            }
        }
    }
}