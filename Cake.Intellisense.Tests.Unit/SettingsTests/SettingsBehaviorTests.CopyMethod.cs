using Cake.Intellisense.Settings;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.SettingsTests
{
    public partial class SettingsBehaviorTests
    {
        public class CopyMethod : Test<SettingsBehavior>
        {
            [Fact]
            public void ShouldReturn_OriginalReference()
            {
                var result = Subject.Copy();

                result.Should().BeSameAs(Subject);
            }
        }
    }
}