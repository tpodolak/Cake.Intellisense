using System;
using System.Runtime.Versioning;
using Cake.MetadataGenerator.NuGet;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.NuGet
{
    public partial class NuGetPackageManagerTests
    {
        public class GetTargetFrameworksMethod : Test<NuGetPackageManager>
        {
            public GetTargetFrameworksMethod()
            {
                Use<IPackage>();
            }

            [Fact]
            public void DeducesTargetFrameworkBasedOnPackageFiles()
            {
                Get<IPackage>().GetFiles().Returns(new[] { Use<IPackageFile>() });
                Get<IPackageFile>().TargetFramework.Returns(new FrameworkName(".NETFramework,Version=v4.5"));

                var result = Subject.GetTargetFrameworks(Get<IPackage>());

                result.Should().NotBeNull();
                result.Should().HaveCount(1);
                result.Should().ContainSingle(framework => framework.FullName == ".NETFramework,Version=v4.5");
            }

            public override object CreateInstance(Type type, params object[] constructorArgs)
            {
                if (type == typeof(INugetPackageRepositoryProvider))
                {
                    var nugetPackageRepositoryProvider = Substitute.For<INugetPackageRepositoryProvider>();
                    nugetPackageRepositoryProvider.Get().Returns(Use<IPackageRepository>());
                    return nugetPackageRepositoryProvider;
                }

                if (type == typeof(INugetPackageManagerProvider))
                {
                    var nugetPackageManagerProvider = Substitute.For<INugetPackageManagerProvider>();
                    nugetPackageManagerProvider.Get().Returns(Use<IPackageManager>());
                    return nugetPackageManagerProvider;
                }
                return base.CreateInstance(type, constructorArgs);
            }
        }
    }
}