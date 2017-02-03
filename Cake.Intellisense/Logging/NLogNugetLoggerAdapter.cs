using System;
using NLog;
using NuGet;
using ILogger = NuGet.ILogger;

namespace Cake.MetadataGenerator.Logging
{
    public class NLogNugetLoggerAdapter : ILogger
    {
        private readonly NLog.ILogger logger;

        public NLogNugetLoggerAdapter(NLog.ILogger logger)
        {
            this.logger = logger;
        }

        public FileConflictResolution ResolveFileConflict(string message)
        {
            return FileConflictResolution.Ignore;
        }

        public void Log(MessageLevel level, string message, params object[] args)
        {
            logger.Log(Convert(level), message, args);
        }

        private LogLevel Convert(MessageLevel level)
        {
            switch (level)
            {
                case MessageLevel.Info:
                    return LogLevel.Info;
                case MessageLevel.Warning:
                    return LogLevel.Warn;
                case MessageLevel.Debug:
                    return LogLevel.Debug;
                case MessageLevel.Error:
                    return LogLevel.Error;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}
