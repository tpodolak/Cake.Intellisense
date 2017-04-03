using System;
using System.Linq;
using Cake.Intellisense.CommandLine.Interfaces;
using Cake.Intellisense.Infrastructure.Interfaces;
using Cake.Intellisense.NuGet.Interfaces;
using NLog;

namespace Cake.Intellisense.CommandLine
{
    public class CommandLineInterface : ICommandLineInterface
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEnvironment _environment;
        private readonly IArgumentParser _argumentParser;
        private readonly IConsoleReader _consoleReader;
        private readonly IPackageManager _packageManager;
        private readonly IHelpScreenGenerator _helpScreenGenerator;

        public CommandLineInterface(
            IArgumentParser argumentParser,
            IConsoleReader consoleReader,
            IPackageManager packageManager,
            IEnvironment environment,
            IHelpScreenGenerator helpScreenGenerator)
        {
            _argumentParser = argumentParser ?? throw new ArgumentNullException(nameof(argumentParser));
            _consoleReader = consoleReader ?? throw new ArgumentNullException(nameof(consoleReader));
            _packageManager = packageManager ?? throw new ArgumentNullException(nameof(packageManager));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _helpScreenGenerator = helpScreenGenerator ?? throw new ArgumentNullException(nameof(helpScreenGenerator));
        }

        public MetadataGeneratorOptions Interact(string[] args)
        {
            var parserResult = _argumentParser.Parse<MetadataGeneratorOptions>(args);

            if (parserResult.Errors.Any())
            {
                Logger.Info(_helpScreenGenerator.Generate<MetadataGeneratorOptions>());
                _environment.Exit(1);
                return null;
            }

            var options = parserResult.Result;

            if (!string.IsNullOrWhiteSpace(options.TargetFramework))
                return options;

            Logger.Info($"TargetFramework not specified. Retrieving target frameworks for package {options.Package} {options.PackageVersion ?? string.Empty}");

            var package = _packageManager.FindPackage(options.Package, options.PackageVersion);

            if (package == null)
            {
                Logger.Error($"Unable to find package {options.Package} {options.PackageVersion ?? string.Empty}");
                _environment.Exit(1);
                return null;
            }

            var frameworks = _packageManager.GetTargetFrameworks(package);
            if (frameworks.Count == 0)
            {
                Logger.Error("Unable to find target frameworks");
                _environment.Exit(1);
                return null;
            }

            int dependencyId = -1;

            Logger.Info("Target frameworks:");
            for (var index = 0; index < frameworks.Count; index++)
            {
                var framework = frameworks[index];
                Logger.Info($"[{index}] - {framework}");
            }

            do
            {
                Logger.Info("Please select framework");
            }
            while (!_consoleReader.TryRead(out dependencyId) && (dependencyId < 0 || dependencyId >= frameworks.Count));

            var targetFramework = frameworks[dependencyId];
            options.TargetFramework = targetFramework.FullName;
            return options;
        }
    }
}