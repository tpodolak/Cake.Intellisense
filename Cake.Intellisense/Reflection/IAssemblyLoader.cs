using System.Collections.Generic;
using System.Reflection;

namespace Cake.MetadataGenerator.Reflection
{
    public interface IAssemblyLoader
    {
        Assembly LoadFrom(string assemblyFile);

        Assembly Load(string assembly);

        IEnumerable<Assembly> LoadReferencedAssemblies(Assembly assembly);

        IEnumerable<Assembly> LoadReferencedAssemblies(string assemblyFile);
    }
}