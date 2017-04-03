using System.Collections.Generic;
using NuGet;

namespace Cake.Intellisense.NuGet.Interfaces
{
    public interface IPackagesIntegrityValidator
    {
        bool IsValid(IList<IPackage> packages);
    }
}