using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.Settings;
using Castle.Components.DictionaryAdapter;
using NuGet;
using IDependencyResolver = Cake.Intellisense.NuGet.IDependencyResolver;
using ISettings = Cake.Intellisense.Settings.ISettings;
using Module = Autofac.Module;

namespace Cake.Intellisense.Infrastructure
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
                   .Where(type => !type.IsAssignableTo<IPackageRepository>())
                   .Except<PhysicalFileSystem>()
                   .Except<DependencyResolver>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
            builder.RegisterInstance(Console.Out).As<TextWriter>();
            builder.RegisterType<PhysicalFileSystem>()
                   .As<IFileSystem>()
                   .WithParameter((parameter, context) => parameter.Position == 0, (parameter, context) => context.Resolve<INuGetSettings>().LocalRepositoryPath)
                   .InstancePerLifetimeScope();
            builder.RegisterType<LocalPackageRepository>().As<IPackageRepository>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<DependencyResolver>().As<IDependencyResolver>()
                   .WithParameter((parameter, context) => parameter.ParameterType == typeof(IPackageRepository), (parameter, context) => context.Resolve<IPackageRepository>())
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