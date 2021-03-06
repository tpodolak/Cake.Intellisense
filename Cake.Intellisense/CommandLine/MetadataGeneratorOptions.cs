﻿using CommandLine;

namespace Cake.Intellisense.CommandLine
{
    public class MetadataGeneratorOptions
    {
        [Option("Package", Required = true, HelpText = "Cake or Cake addin package")]
        public string Package { get; set; }

        [Option("PackageVersion", Required = false, HelpText = "Package version")]
        public string PackageVersion { get; set; }

        [Option("OutputFolder", Required = false, HelpText = "Output folder for generated libraries")]
        public string OutputFolder { get; set; }

        [Option("TargetFramework", Required = false, HelpText = "Package target framework")]
        public string TargetFramework { get; set; }
    }
}
