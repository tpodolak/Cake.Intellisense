using System.Collections.Generic;
using Cake.Intellisense.NuGet;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using NSubstitute;
using NuGet;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.NuGetTests
{
    public partial class PackagesIntegrityValidatorTests
    {
        public class IsValidMethod : Test<PackagesIntegrityValidator>
        {
            [Fact]
            public void ReturnsTrue_WhenPackageList_ContainsCakeCorePackage()
            {
                var package = Use<IPackage>();
                package.Id.Returns("Cake.Core");

                var result = Subject.IsValid(new List<IPackage> { package });

                result.Should().BeTrue();
            }

            [Fact]
            public void ReturnsFalse_WhenPackageList_DoesNotContainCakeCorePackage()
            {
                var package = Use<IPackage>();
                package.Id.Returns("Cake.Common");

                var result = Subject.IsValid(new List<IPackage> { package });

                result.Should().BeFalse();
            }
        }
    }
}