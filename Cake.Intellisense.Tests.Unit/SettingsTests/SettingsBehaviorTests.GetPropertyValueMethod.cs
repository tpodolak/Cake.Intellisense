using System;
using Cake.Intellisense.Settings;
using Cake.Intellisense.Tests.Unit.Common;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.SettingsTests
{
    public partial class SettingsBehaviorTests
    {
        public class GetPropertyValueMethod : Test<SettingsBehavior>
        {
            [Fact]
            public void ReturnsStoredValue_WhenValueExists()
            {
                var storedValue = new object();

                var result = Subject.GetPropertyValue(
                    Use<IDictionaryAdapter>(),
                    string.Empty,
                    storedValue,
                    new PropertyDescriptor(),
                    false);

                result.Should().BeSameAs(storedValue);
            }

            [Fact]
            public void ThrowsInvalidOperationException_WhenStoredValueNull()
            {
                object storedValue = null;

                Subject.Invoking(sub => sub.GetPropertyValue(
                        Use<IDictionaryAdapter>(),
                        string.Empty,
                        storedValue,
                        new PropertyDescriptor(),
                        false)).ShouldThrow<InvalidOperationException>();
            }
        }
    }
}