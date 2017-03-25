using System;
using System.Linq;
using System.Runtime.Versioning;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.Settings.Interfaces;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;
using IDependencyResolver = NuGet.IDependencyResolver;

namespace Cake.Intellisense.Tests.Unit.NuGetTests
{
    public class DependencyResolverTests
    {
        public class GetDependenciesAndSelfMethod : Test<DependencyResolver>
        {
            private readonly FrameworkName _defaultFramework = new FrameworkName(".NETFramework,Version=v4.5");

            public GetDependenciesAndSelfMethod()
            {
                Use<IPackage>();
                Get<INuGetSettings>().RecursiveDependencyResolution.Returns(true);
            }

            [Fact]
            public void ReturnsOriginalPackage_WhenPackageHasNoDependencies()
            {
                var package = Get<IPackage>();
                package.DependencySets.Returns(Enumerable.Empty<PackageDependencySet>());

                var result = Subject.GetDependenciesAndSelf(package, _defaultFramework);

                result.Should().HaveCount(1);
                result.ShouldBeEquivalentTo(new[] { package });
            }

            [Fact]
            public void ReturnsOriginalPackage_WhenPackageHasNoDependenciesSupportingGivenFramework()
            {
                var package = Get<IPackage>();
                var dependentPackage = Substitute.For<IPackage>();
                var packageDependencySets = new[] { CreateDependencySet(dependentPackage) };
                package.DependencySets.Returns(packageDependencySets);

                ((IDependencyResolver)Get<IPackageRepository>()).ResolveDependency(
                    Arg.Any<PackageDependency>(),
                    Arg.Any<IPackageConstraintProvider>(),
                    Arg.Any<bool>(),
                    Arg.Any<bool>(),
                    Arg.Any<DependencyVersion>()).Returns(dependentPackage);

                var result = Subject.GetDependenciesAndSelf(package, new FrameworkName(".NETFramework,Version=v4.6")).ToList();

                result.Should().HaveCount(1);
                result.ShouldBeEquivalentTo(new[] { package });
            }

            [Fact]
            public void RecursivelySeachForAllPackagesSupportingGivenFramework_WhenRecursiveDependencyResolutionEnabled()
            {
                var package = Substitute.For<IPackage>();
                var firstDependentPackage = Substitute.For<IPackage>();
                var secondDependentPackage = Substitute.For<IPackage>();
                var secondLevelDependencyPackage = Substitute.For<IPackage>();
                var packageDependencySets = new[] { CreateDependencySet(firstDependentPackage), CreateDependencySet(secondDependentPackage) };
                package.DependencySets.Returns(packageDependencySets);
                var dependencySets = new[] { CreateDependencySet(secondLevelDependencyPackage) };
                firstDependentPackage.DependencySets.Returns(dependencySets);
                ((IDependencyResolver)Get<IPackageRepository>()).ResolveDependency(
                    Arg.Any<PackageDependency>(),
                    Arg.Any<IPackageConstraintProvider>(),
                    Arg.Any<bool>(),
                    Arg.Any<bool>(),
                    Arg.Any<DependencyVersion>()).Returns(firstDependentPackage, secondDependentPackage, secondLevelDependencyPackage);

                var result = Subject.GetDependenciesAndSelf(package, _defaultFramework).ToList();

                result.Should().HaveCount(4);
                result.ShouldBeEquivalentTo(new[] { package, firstDependentPackage, secondDependentPackage, secondLevelDependencyPackage });
            }

            [Fact]
            public void FlatSeachForAllPackagesSupportingGivenFramework_WhenRecursiveDependencyResolutionDisabled()
            {
                var package = Substitute.For<IPackage>();
                var firstDependentPackage = Substitute.For<IPackage>();
                var secondDependentPackage = Substitute.For<IPackage>();
                var secondLevelDependencyPackage = Substitute.For<IPackage>();
                var packageDependencySets = new[] { CreateDependencySet(firstDependentPackage), CreateDependencySet(secondDependentPackage) };
                package.DependencySets.Returns(packageDependencySets);
                var dependencySets = new[] { CreateDependencySet(secondLevelDependencyPackage) };
                firstDependentPackage.DependencySets.Returns(dependencySets);
                ((IDependencyResolver)Get<IPackageRepository>()).ResolveDependency(
                    Arg.Any<PackageDependency>(),
                    Arg.Any<IPackageConstraintProvider>(),
                    Arg.Any<bool>(),
                    Arg.Any<bool>(),
                    Arg.Any<DependencyVersion>()).Returns(firstDependentPackage, secondDependentPackage, secondLevelDependencyPackage);
                Get<INuGetSettings>().RecursiveDependencyResolution.Returns(false);

                var result = Subject.GetDependenciesAndSelf(package, _defaultFramework).ToList();

                result.Should().HaveCount(3);
                result.ShouldBeEquivalentTo(new[] { package, firstDependentPackage, secondDependentPackage });
            }

            public override object CreateInstance(Type type, params object[] constructorArgs)
            {
                if (type == typeof(IPackageRepository))
                {
                    var nugetPackageRepositoryProvider = Substitute.For<IPackageRepository, IDependencyResolver>();
                    return nugetPackageRepositoryProvider;
                }

                return base.CreateInstance(type, constructorArgs);
            }

            private PackageDependencySet CreateDependencySet(IPackage package)
            {
                var packageAssemblyReference = Substitute.For<IPackageAssemblyReference>();
                packageAssemblyReference.Path.Returns("lib.dll");

                packageAssemblyReference.SupportedFrameworks.Returns(new[] { _defaultFramework });

                package.AssemblyReferences.Returns(new[] { packageAssemblyReference });
                return new PackageDependencySet(_defaultFramework, new[] { new PackageDependency("id") });
            }
        }
    }
}