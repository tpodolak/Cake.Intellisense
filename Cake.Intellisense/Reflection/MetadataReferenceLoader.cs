using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using Cake.Intellisense.NuGet.Interfaces;
using Cake.Intellisense.Reflection.Interfaces;
using Microsoft.CodeAnalysis;
using MoreLinq;
using NuGet;

namespace Cake.Intellisense.Reflection
{
    public class MetadataReferenceLoader : IMetadataReferenceLoader
    {
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly IPackageAssemblyResolver _packageAssemblyResolver;

        public MetadataReferenceLoader(IAssemblyLoader assemblyLoader, IPackageAssemblyResolver packageAssemblyResolver)
        {
            _assemblyLoader = assemblyLoader ?? throw new ArgumentNullException(nameof(assemblyLoader));
            _packageAssemblyResolver = packageAssemblyResolver ?? throw new ArgumentNullException(nameof(packageAssemblyResolver));
        }

        public PortableExecutableReference CreateFromFile(string path)
        {
            return MetadataReference.CreateFromFile(path);
        }

        public IList<PortableExecutableReference> CreateFromPackages(IList<IPackage> packages, FrameworkName targetFramework)
        {
            var packageAssemblies = packages.SelectMany(package => _packageAssemblyResolver.ResolveAssemblies(package, targetFramework)).ToList();

            var referencedAssemblyReferences =
                packageAssemblies
                    .SelectMany(packageFile => _assemblyLoader.LoadReferencedAssemblies(packageFile))
                    .Select(assembly => CreateFromFile(assembly.Location))
                    .ToList();

            var physicalPackagesReferences = packageAssemblies.Select(val => CreateFromFile(val.Location));
            return referencedAssemblyReferences.Union(physicalPackagesReferences).DistinctBy(reference => Path.GetFileName(reference.FilePath)).ToList();
        }
    }
}