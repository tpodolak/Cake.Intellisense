using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Cake.MetadataGenerator.Reflection
{
    public interface IAssemblyLoader
    {
        Assembly LoadFrom(string assemblyFile);

        Assembly Load(Stream stream);

        Assembly Load(string assembly);

        IEnumerable<Assembly> LoadReferencedAssemblies(Assembly assembly);

        IEnumerable<Assembly> LoadReferencedAssemblies(string assemblyFile);

        IEnumerable<Assembly> LoadReferencedAssemblies(Stream stream);
    }
}