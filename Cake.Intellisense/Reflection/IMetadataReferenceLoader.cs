using System.Collections.Generic;
using System.IO;
using System.Runtime.Versioning;
using Microsoft.CodeAnalysis;
using NuGet;

namespace Cake.Intellisense.Reflection
{
    public interface IMetadataReferenceLoader
    {
        PortableExecutableReference CreateFromFile(string path);

        IList<PortableExecutableReference> CreateFromPackages(IList<IPackage> packages, FrameworkName targetFramework);
    }
}