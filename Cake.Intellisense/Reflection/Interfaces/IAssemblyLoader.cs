using System.Collections.Generic;
using System.Reflection;

namespace Cake.Intellisense.Reflection.Interfaces
{
    public interface IAssemblyLoader
    {
        Assembly LoadFrom(string assemblyFile);

        Assembly Load(string assembly);

        IEnumerable<Assembly> LoadReferencedAssemblies(Assembly assembly);

        IEnumerable<Assembly> LoadReferencedAssemblies(string assemblyFile);
    }
}