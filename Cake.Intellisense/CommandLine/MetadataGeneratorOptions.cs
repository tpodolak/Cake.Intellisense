using CommandLine;

namespace Cake.MetadataGenerator.CommandLine
{
    public class MetadataGeneratorOptions
    {
        [Option("Package", Required = true, HelpText = "Cake/Cake plugin package")]
        public string Package { get; set; }

        [Option("PackageVersion", Required = false, HelpText = "Cake/Cake plugin package version")]
        public string PackageVersion { get; set; }
        
        [Option("OutputFolder", Required = false, HelpText = "Output folder for generated dlls")]
        public string OutputFolder { get; set; }

        [Option("PackageFrameworkTargetVersion", Required = false, HelpText = "Package's framework version")]
        public string PackageFrameworkTargetVersion { get; set; }
    }
}
