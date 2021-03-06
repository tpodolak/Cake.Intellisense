﻿using Cake.Intellisense.Settings;
using Cake.Intellisense.Tests.Unit.Common;
using FluentAssertions;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.SettingsTests
{
    public partial class SettingsBehaviorTests
    {
        public class ExecutionOrderProperty : Test<SettingsBehavior>
        {
            [Fact]
            public void ShouldReturn_IntMaxValue()
            {
                var result = Subject.ExecutionOrder;

                result.Should().Be(int.MaxValue);
            }
        }
    }
}