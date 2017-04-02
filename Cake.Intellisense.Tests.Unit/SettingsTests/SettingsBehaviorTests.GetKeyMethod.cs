using System.Collections.Generic;
using Cake.Intellisense.Settings;
using Cake.Intellisense.Settings.Interfaces;
using Cake.Intellisense.Tests.Unit.Common;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.SettingsTests
{
    public partial class SettingsBehaviorTests
    {
        public class GetKeyMethod : Test<SettingsBehavior>
        {
            [Fact]
            public void ReturnsKey_FollowingNamingConventions()
            {
                var adapter = Use<IDictionaryAdapter>();

                var dictionaryAdapterMeta = new DictionaryAdapterMeta(
                    typeof(INuGetSettings),
                    typeof(INuGetSettings),
                    new object[0],
                    new IDictionaryMetaInitializer[0],
                    new IDictionaryInitializer[0],
                    new Dictionary<string, PropertyDescriptor>(),
                    Use<IDictionaryAdapterFactory>(),
                    instance => adapter);

                adapter.Meta.Returns(dictionaryAdapterMeta);

                var result = Subject.GetKey(adapter, "PackageSource", null);

                result.Should().Be("NuGetSettings.PackageSource");
            }
        }
    }
}