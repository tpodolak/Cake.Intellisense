using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cake.MetadataGenerator
{
    public class GeneratorResult
    {
        public Assembly[] SourceAssemblies { get; set; }

        public Assembly EmitedAssembly { get; set; }
    }
}
