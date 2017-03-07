using System;
using System.Runtime.Versioning;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;
using IPackageManager = NuGet.IPackageManager;

namespace Cake.Intellisense.Tests.Unit.NuGetTests
{
    public partial class PackageManagerTests
    {
        public class GetTargetFrameworksMethod : Test<NuGet.PackageManager>
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
                if (type == typeof(IPackageRepositoryProvider))
                {
                    var nugetPackageRepositoryProvider = Substitute.For<IPackageRepositoryProvider>();
                    nugetPackageRepositoryProvider.Get().Returns(Use<IPackageRepository>());
                    return nugetPackageRepositoryProvider;
                }

                if (type == typeof(IPackageManagerProvider))
                {
                    var nugetPackageManagerProvider = Substitute.For<IPackageManagerProvider>();
                    nugetPackageManagerProvider.Get().Returns(Use<IPackageManager>());
                    return nugetPackageManagerProvider;
                }

                return base.CreateInstance(type, constructorArgs);
            }
        }
    }
}