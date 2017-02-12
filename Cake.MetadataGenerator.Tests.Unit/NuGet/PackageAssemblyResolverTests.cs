using Cake.MetadataGenerator.NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.NuGet
{
    // TODO Fix PhysicalPackage issue to write tests
    public class PackageAssemblyResolverTests : Test<PackageAssemblyResolver>
    {
        [Fact]
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