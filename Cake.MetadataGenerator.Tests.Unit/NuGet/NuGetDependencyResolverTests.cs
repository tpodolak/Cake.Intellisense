using System;
using System.Linq;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.NuGet;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.NuGet
{
    public class NuGetDependencyResolverTests
    {
        public class GetDependentPackagesAndSelfMethod : Test<NuGetDependencyResolver>
        {
            private readonly FrameworkName defaultFramework = new FrameworkName(".NETFramework,Version=v4.5");

            public GetDependentPackagesAndSelfMethod()
            {
                Use<IPackage>();
            }

            [Fact]
            public void RetursEmptyListWhenPackageNull()
            {
                var result = Subject.GetDependentPackagesAndSelf(null, defaultFramework);

                result.Should().BeEmpty();
            }

            [Fact]
            public void ReturnsOriginalPackageWhenPackageHasNoDependencies()
            {
                var package = Get<IPackage>();
                package.DependencySets.Returns(Enumerable.Empty<PackageDependencySet>());

                var result = Subject.GetDependentPackagesAndSelf(package, defaultFramework);

                result.Should().HaveCount(1);
                result.ShouldBeEquivalentTo(new[] { package });
            }

            [Fact]
            public void ReturnsOriginalPackageWhenPackageHasNoDependenciesSuppotingGivenFramework()
            {
                var package = Get<IPackage>();
                var dependentPackage = Substitute.For<IPackage>();
                var packageDependencySets = new[] { CreateDependencySet(dependentPackage) };
                package.DependencySets.Returns(packageDependencySets);

                ((IDependencyResolver)Get<IPackageRepository>()).ResolveDependency(Arg.Any<PackageDependency>(),
                    Arg.Any<IPackageConstraintProvider>(), Arg.Any<bool>(), Arg.Any<bool>(),
                    Arg.Any<DependencyVersion>()).Returns(dependentPackage);

                var result = Subject.GetDependentPackagesAndSelf(package, new FrameworkName(".NETFramework,Version=v4.6"));

                result.Should().HaveCount(1);
                result.ShouldBeEquivalentTo(new[] { package });
            }

            [Fact]
            public void RecursivelySeachForAllPackagesSupportingGivenFramework()
            {
                var package = Substitute.For<IPackage>();
                var firstDependentPackage = Substitute.For<IPackage>();
                var secondDependentPackage = Substitute.For<IPackage>();
                var secondLevelDependencyPackage = Substitute.For<IPackage>();
                var packageDependencySets = new[] { CreateDependencySet(firstDependentPackage), CreateDependencySet(secondDependentPackage) };
                package.DependencySets.Returns(packageDependencySets);
                var dependencySets = new[] { CreateDependencySet(secondLevelDependencyPackage) };
                firstDependentPackage.DependencySets.Returns(dependencySets);
                ((IDependencyResolver)Get<IPackageRepository>()).ResolveDependency(Arg.Any<PackageDependency>(),
                    Arg.Any<IPackageConstraintProvider>(), Arg.Any<bool>(), Arg.Any<bool>(),
                    Arg.Any<DependencyVersion>()).Returns(firstDependentPackage, secondDependentPackage, secondLevelDependencyPackage);

                var result = Subject.GetDependentPackagesAndSelf(package, defaultFramework).ToList();

                result.Should().HaveCount(4);
                result.ShouldBeEquivalentTo(new[] { package, firstDependentPackage, secondDependentPackage, secondLevelDependencyPackage });
            }

            private PackageDependencySet CreateDependencySet(IPackage package)
            {
                var packageFile = Substitute.For<IPackageFile>();
                packageFile.Path.Returns("lib.dll");

                packageFile.SupportedFrameworks.Returns(new[] { defaultFramework });

                package.GetFiles().Returns(new[] { packageFile });
                return new PackageDependencySet(defaultFramework, new[] { new PackageDependency("id") });
            }

            public override object CreateInstance(Type type, params object[] constructorArgs)
            {
                if (type == typeof(INugetPackageRepositoryProvider))
                {
                    var nugetPackageRepositoryProvider = Substitute.For<INugetPackageRepositoryProvider>();
                    nugetPackageRepositoryProvider.Get().Returns(Use(Substitute.For<IPackageRepository, IDependencyResolver>()));
                    return nugetPackageRepositoryProvider;
                }

                return base.CreateInstance(type, constructorArgs);
            }
        }
    }
}