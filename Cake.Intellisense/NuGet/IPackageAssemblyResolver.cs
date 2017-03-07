using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Versioning;
using NuGet;

namespace Cake.Intellisense.NuGet
{
    public interface IPackageAssemblyResolver
    {
        List<Assembly> ResolveAssemblies(IPackage package, FrameworkName targetFramework);
    }
}