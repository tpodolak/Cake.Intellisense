using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Cake.Intellisense.NuGet.Interfaces;
using Cake.Intellisense.Reflection;
using Cake.Intellisense.Tests.Unit.Common;
using NSubstitute;
using NuGet;
using Xunit;
using IPackageManager = Cake.Intellisense.NuGet.Interfaces.IPackageManager;

namespace Cake.Intellisense.Tests.Unit.ReflectionTests
{
    public class MetadataReferenceLoaderTests
    {
        public class CreateFromFileMethod : Test<MetadataReferenceLoader>
        {
            [Fact]
            public void AddsCakeCore_ToPackageList_WhenPackageIntegrityIsNotValid()
            {
                var corePackage = Substitute.For<IPackage>();
                var packages = new List<IPackage> { Use<IPackage>() };
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");

                Get<IPackagesIntegrityValidator>().IsValid(Arg.Any<IList<IPackage>>()).Returns(false);
                Get<IPackageManager>().InstallPackage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<FrameworkName>())
                                      .Returns(corePackage);
                Get<IPackageAssemblyResolver>().ResolveAssemblies(Arg.Any<IPackage>(), Arg.Any<FrameworkName>())
                                               .Returns(new List<Assembly>());

                Subject.CreateFromPackages(packages, targetFramework);

                Get<IPackageManager>()
                    .Received(1)
                    .InstallPackage(
                        Arg.Is<string>(id => id == Constants.Packages.CakeCore),
                        Arg.Is<string>(version => version == null),
                        Arg.Is<FrameworkName>(framework => framework == targetFramework));

                Get<IPackageAssemblyResolver>()
                    .Received(1)
                    .ResolveAssemblies(Arg.Is<IPackage>(package => package == corePackage), Arg.Any<FrameworkName>());

                Get<IPackageAssemblyResolver>()
                    .Received(1)
                    .ResolveAssemblies(Arg.Is<IPackage>(package => packages.Contains(package)), Arg.Any<FrameworkName>());
            }

            [Fact]
            public void DoesNotAddCakeCore_ToPackageList_WhenPackageIntegrityIsValid()
            {
                var corePackage = Substitute.For<IPackage>();
                var packages = new List<IPackage> { Use<IPackage>() };
                var targetFramework = new FrameworkName(".NETFramework,Version=v4.5");

                Get<IPackagesIntegrityValidator>().IsValid(Arg.Any<IList<IPackage>>()).Returns(true);
                Get<IPackageManager>().InstallPackage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<FrameworkName>())
                    .Returns(corePackage);
                Get<IPackageAssemblyResolver>().ResolveAssemblies(Arg.Any<IPackage>(), Arg.Any<FrameworkName>())
                    .Returns(new List<Assembly>());

                Subject.CreateFromPackages(packages, targetFramework);

                Get<IPackageManager>()
                    .DidNotReceive()
                    .InstallPackage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<FrameworkName>());

                Get<IPackageAssemblyResolver>()
                    .Received(1)
                    .ResolveAssemblies(Arg.Any<IPackage>(), Arg.Any<FrameworkName>());
            }
        }
    }
}