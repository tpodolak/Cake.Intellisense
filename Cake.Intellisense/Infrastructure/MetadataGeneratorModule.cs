using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Cake.Intellisense.Logging;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.Settings;
using Cake.Intellisense.Settings.Interfaces;
using Castle.Components.DictionaryAdapter;
using NLog;
using NuGet;
using IDependencyResolver = Cake.Intellisense.NuGet.Interfaces.IDependencyResolver;
using ISettings = Cake.Intellisense.Settings.Interfaces.ISettings;
using Module = Autofac.Module;

namespace Cake.Intellisense.Infrastructure
{
    public class MetadataGeneratorModule : Module
    {
        private readonly NameValueCollection _appSettings;
        private readonly Assembly _assembly;

        public MetadataGeneratorModule(NameValueCollection appSettings, Assembly assembly)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        protected override void Load(ContainerBuilder builder)
        {
            var referencedAssemblies = _assembly.GetReferencedAssemblies().Select(Assembly.Load);

            builder.RegisterInstance(Console.Out).As<TextWriter>();
            RegisterSettings(builder);

            builder.RegisterAssemblyTypes(referencedAssemblies.Union(new[] { _assembly }).ToArray())
                   .Where(type => !type.IsAssignableTo<IPackageRepository>() && !type.IsAssignableTo<IPackageManager>())
                   .Except<PhysicalFileSystem>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<PhysicalFileSystem>()
                   .As<IFileSystem>()
                   .WithParameter((parameter, context) => parameter.Position == 0, (parameter, context) => context.Resolve<INuGetSettings>().LocalRepositoryPath)
                   .OnActivated(args => args.Instance.Logger = new NLogNugetLoggerAdapter(LogManager.GetLogger(args.Instance.GetType().FullName)))
                   .InstancePerLifetimeScope();

            builder.RegisterType<LocalPackageRepository>()
                   .Keyed<IPackageRepository>(nameof(LocalPackageRepository))
                   .InstancePerLifetimeScope();

            builder.Register(componentContext => componentContext.Resolve<IPackageRepositoryFactory>().CreateRepository(componentContext.Resolve<INuGetSettings>().PackageSource))
                   .As<IPackageRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<global::NuGet.PackageManager>()
                   .As<IPackageManager>()
                   .UsingConstructor(typeof(IPackageRepository), typeof(IPackagePathResolver), typeof(IFileSystem))
                   .OnActivated(args => args.Instance.Logger = new NLogNugetLoggerAdapter(LogManager.GetLogger(args.Instance.GetType().FullName)))
                   .InstancePerLifetimeScope();

            builder.RegisterType<DependencyResolver>()
                   .As<IDependencyResolver>()
                   .WithParameter((parameter, context) => parameter.ParameterType == typeof(IPackageRepository), (parameter, context) => context.ResolveKeyed<IPackageRepository>(nameof(LocalPackageRepository)))
                   .InstancePerLifetimeScope();
        }

        private void RegisterSettings(ContainerBuilder builder)
        {
            var factory = new DictionaryAdapterFactory();
            var appSettingsAdapter = new NameValueCollectionAdapter(_appSettings);
            var descriptor = new PropertyDescriptor().AddBehavior(new SettingsBehavior());
            var settingsType = typeof(ISettings);

            foreach (var type in _assembly.ExportedTypes.Where(val => val.IsInterface && val.IsAssignableTo<ISettings>() && val != settingsType))
            {
                builder.RegisterInstance(factory.GetAdapter(type, appSettingsAdapter, descriptor)).As(type);
            }
        }
    }
}