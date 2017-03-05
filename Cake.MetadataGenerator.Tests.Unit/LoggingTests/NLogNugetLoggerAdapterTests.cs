using System.Collections.Generic;
using Cake.MetadataGenerator.Logging;
using Cake.MetadataGenerator.Tests.Unit.Common;
using FluentAssertions;
using NLog;
using NSubstitute;
using NuGet;
using Xunit;
using ILogger = NLog.ILogger;

namespace Cake.MetadataGenerator.Tests.Unit.LoggingTests
{
    public partial class NLogNugetLoggerAdapterTests
    {
        public class LogMethod : Test<NLogNugetLoggerAdapter>
        {
            public static IEnumerable<object[]> LevelPairs
            {
                get
                {
                    yield return new object[] { MessageLevel.Info, LogLevel.Info };
                    yield return new object[] { MessageLevel.Debug, LogLevel.Debug };
                    yield return new object[] { MessageLevel.Error, LogLevel.Error };
                    yield return new object[] { MessageLevel.Warning, LogLevel.Warn };
                }
            }

            [Theory]
            [MemberData(nameof(LevelPairs))]
            public void CallsNLogLoggerWithProperLogLevel(MessageLevel messageLevel, LogLevel logLevel)
            {
                Subject.Log(messageLevel, "message");

                Get<ILogger>().Log(Arg.Is<LogLevel>(level => level == logLevel), Arg.Any<string>());
            }
        }

        public class ResolveFileConflictMethod : Test<NLogNugetLoggerAdapter>
        {
            [Theory]
            [InlineData("")]
            [InlineData(" ")]
            [InlineData(null)]
            [InlineData("message")]
            public void AlwaysReturnsFileConflictResolutionIgnore(string message)
            {
                var result = Subject.ResolveFileConflict(message);

                result.Should().Be(FileConflictResolution.Ignore);
            }
        }
    }
}