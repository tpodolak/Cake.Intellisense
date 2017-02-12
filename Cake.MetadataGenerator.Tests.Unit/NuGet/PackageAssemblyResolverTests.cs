using Cake.MetadataGenerator.NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.NuGet
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