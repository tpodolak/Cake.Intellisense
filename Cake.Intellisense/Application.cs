using System;
using System.Configuration;
using System.Reflection;
using Autofac;
using Cake.Intellisense.CommandLine;
using Cake.Intellisense.CommandLine.Interfaces;
using Cake.Intellisense.Infrastructure;
using NLog;

namespace Cake.Intellisense
{
    public class Application
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public GeneratorResult Run(string[] args)
        {
            var builder = new ContainerBuilder();
            var module = new MetadataGeneratorModule(ConfigurationManager.AppSettings, Assembly.GetExecutingAssembly());
            builder.RegisterModule(module);
            var container = builder.Build();

            try
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var metadataGenerator = scope.Resolve<IMetadataGenerator>();
                    var commandLine = scope.Resolve<ICommandLineInterface>();
                    var options = commandLine.Interact(args);
                    return metadataGenerator.Generate(options);
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                return null;
            }
            finally
            {
                container.Dispose();
            }
        }
    }
}