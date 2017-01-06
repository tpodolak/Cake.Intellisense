using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Cake.Intellisense
{
    public class CommandLineOptions
    {
        [Option("Package", Required = true, HelpText = "Cake/Cake plugin package")]
        public string Package { get; set; }

        [Option("PackageVersion", Required = false, HelpText = "Cake/Cake plugin package version")]
        public string PackageVersion { get; set; }

        [Option("AssemblyName", Required = true, HelpText = "Assembly to be processed")]
        public string AssemblyName { get; set; }
    }
}
