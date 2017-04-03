using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Intellisense.NuGet.Interfaces;
using NuGet;

namespace Cake.Intellisense.NuGet
{
    public class PackagesIntegrityValidator : IPackagesIntegrityValidator
    {
        public bool IsValid(IList<IPackage> packages)
        {
            return packages.Any(package => package.Id.Equals(Constants.Packages.CakeCore, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}