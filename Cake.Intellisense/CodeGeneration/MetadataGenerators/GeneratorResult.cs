using System;
using System.Collections.Generic;
using System.Reflection;

namespace Cake.Intellisense.CodeGeneration.MetadataGenerators
{
    [Serializable]
    public class GeneratorResult
    {
        public List<Assembly> SourceAssemblies { get; set; } = new List<Assembly>();

        public List<Assembly> EmitedAssemblies { get; set; } = new List<Assembly>();
    }
}
