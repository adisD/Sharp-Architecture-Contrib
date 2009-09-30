using System;
using log4net;

namespace SharpArchContrib.Core.Logging {
    public static class Log4NetHelper {
        public static void Log(this ILog logger, LoggingLevel level, string message) {
            switch (level) {
                case LoggingLevel.All:
                case LoggingLevel.Debug:
                    logger.Debug(message);
                    break;
                case LoggingLevel.Info:
                    logger.Info(message);
                    break;
                case LoggingLevel.Warn:
                    logger.Warn(message);
                    break;
                case LoggingLevel.Error:
                    logger.Error(message);
                    break;
                case LoggingLevel.Fatal:
                    logger.Fatal(message);
                    break;
            }
        }

        public static void Log(this ILog logger, LoggingLevel level, Exception err) {
            switch (level)
            {
                case LoggingLevel.All:
                case LoggingLevel.Debug:
                    logger.Debug(err);
                    break;
                case LoggingLevel.Info:
                    logger.Info(err);
                    break;
                case LoggingLevel.Warn:
                    logger.Warn(err);
                    break;
                case LoggingLevel.Error:
                    logger.Error(err);
                    break;
                case LoggingLevel.Fatal:
                    logger.Fatal(err);
                    break;
            }
        }

        public static bool IsEnabledFor(this ILog logger, LoggingLevel level) {
            switch (level)
            {
                case LoggingLevel.All:
                    return true;
                case LoggingLevel.Debug:
                    return logger.IsDebugEnabled;
                case LoggingLevel.Info:
                    return logger.IsInfoEnabled;
                case LoggingLevel.Warn:
                    return logger.IsWarnEnabled;
                case LoggingLevel.Error:
                    return logger.IsErrorEnabled;
                case LoggingLevel.Fatal:
                    return logger.IsFatalEnabled;
            }

            return false;
        }
    }
}