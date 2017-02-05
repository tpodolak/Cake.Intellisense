﻿using System.Linq;
using Cake.MetadataGenerator.NuGet;
using NLog;

namespace Cake.MetadataGenerator.CommandLine
{
    public class CommandLineInterface : ICommandLineInterface
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IArgumentParser argumentParser;
        private readonly IConsoleReader consoleReader;
        private readonly INuGetPackageManager packageManager;

        public CommandLineInterface(IArgumentParser argumentParser,
            IConsoleReader consoleReader,
            INuGetPackageManager packageManager)
        {
            this.argumentParser = argumentParser;
            this.consoleReader = consoleReader;
            this.packageManager = packageManager;
        }

        public MetadataGeneratorOptions Interact(string[] args)
        {
            var parserResult = argumentParser.Parse<MetadataGeneratorOptions>(args);

            if (parserResult.Errors.Any())
            {
                Logger.Error("Error while parsing arguments");
                return null;
            }

            var options = parserResult.Result;

            if (!string.IsNullOrWhiteSpace(options.TargetFramework))
                return options;

            Logger.Info($"TargetFramework not specified. Retrieving target frameworks for package {options.Package} {options.PackageVersion ?? string.Empty}");

            var package = packageManager.FindPackage(options.PackageVersion, options.PackageVersion);

            if (package == null)
            {
                Logger.Error($"Unable to find package {options.Package} {options.PackageVersion ?? string.Empty}");
                return null;
            }

            var frameworks = packageManager.GetTargetFrameworks(package);

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
            while (!consoleReader.TryRead(out dependencyId) && (dependencyId < 0 || dependencyId >= frameworks.Count));


            var targetFramework = frameworks[dependencyId];
            options.TargetFramework = targetFramework.FullName;
            return options;
        }
    }
}