using Cake.Intellisense.Settings;
using Cake.Intellisense.Tests.Unit.Common;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.SettingsTests
{
    public partial class SettingsBehaviorTests
    {
        public class InitializeMethod : Test<SettingsBehavior>
        {
            [Fact]
            public void SetsPropertyDescriptor_FetchProperty_ToTrue()
            {
                var descriptor = new PropertyDescriptor();

                Subject.Initialize(descriptor, null);

                descriptor.Fetch.Should().BeTrue();
            }
        }
    }
}