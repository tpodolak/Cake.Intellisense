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
using IPackageManager = Cake.Intellisense.NuGet.Interfaces.IPackageManager;

namespace Cake.Intellisense.Reflection
{
    public class MetadataReferenceLoader : IMetadataReferenceLoader
    {
        private readonly IPackageManager _packageManager;
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly IPackageAssemblyResolver _packageAssemblyResolver;
        private readonly IPackagesIntegrityValidator _packagesIntegrityValidator;

        public MetadataReferenceLoader(
            IAssemblyLoader assemblyLoader,
            IPackageAssemblyResolver packageAssemblyResolver,
            IPackagesIntegrityValidator packagesIntegrityValidator,
            IPackageManager packageManager)
        {
            _assemblyLoader = assemblyLoader ?? throw new ArgumentNullException(nameof(assemblyLoader));
            _packageAssemblyResolver = packageAssemblyResolver ?? throw new ArgumentNullException(nameof(packageAssemblyResolver));
            _packagesIntegrityValidator = packagesIntegrityValidator ?? throw new ArgumentNullException(nameof(packagesIntegrityValidator));
            _packageManager = packageManager ?? throw new ArgumentNullException(nameof(packageManager));
        }

        public PortableExecutableReference CreateFromFile(string path)
        {
            return MetadataReference.CreateFromFile(path);
        }

        public IList<PortableExecutableReference> CreateFromPackages(IList<IPackage> packages, FrameworkName targetFramework)
        {
            packages = packages.ToList();

            if (!_packagesIntegrityValidator.IsValid(packages))
            {
                var cakeCore = _packageManager.InstallPackage(Constants.Packages.CakeCore, null, targetFramework);
                packages.Add(cakeCore);
            }

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