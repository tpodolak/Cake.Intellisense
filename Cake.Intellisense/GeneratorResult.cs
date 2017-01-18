using System.Reflection;

namespace Cake.MetadataGenerator
{
    public class GeneratorResult
    {
        public Assembly[] SourceAssemblies { get; set; }

        public Assembly EmitedAssembly { get; set; }
    }
}
