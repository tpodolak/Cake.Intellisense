using CommandLine;

namespace Cake.MetadataGenerator
{
    public class MetadataGeneratorOptions
    {
        [Option("Package", Required = true, HelpText = "Cake package")]
        public string Package { get; set; }

        [Option("PackageVersion", Required = false, HelpText = "Package version")]
        public string PackageVersion { get; set; }
        
        [Option("OutputFolder", Required = false, HelpText = "Output folder for generated dlls")]
        public string OutputFolder { get; set; }

        [Option("TargetFramework", Required = false, HelpText = "Package's target framework")]
        public string TargetFramework { get; set; }
    }
}
