using Cake.MetadataGenerator.NuGet;
using Cake.MetadataGenerator.Tests.Unit.Common;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.NuGetTests
{
    // TODO Fix PhysicalPackage issue to write tests
    public partial class PackageAssemblyResolverTests : Test<PackageAssemblyResolver>
    {
        public class ResolveAssemblies : Test<PackageAssemblyResolver>
        {
            [Fact(Skip = "Unable to write proper unit tests due to PhysicalPackage issue")]
            public void ResolveAssembliesResolvesAssembliesBasedOnPhysicalPackagesSupportingGivenFramework()
            {
                //            Use<IPackage>().GetFiles().Returns(new List<IPackageFile>
                //            {
                //                Use<IPackageFile>().SupportedFrameworks
                //            })
                //            Subject.ResolveAssemblies()
            }
        }
    }
}