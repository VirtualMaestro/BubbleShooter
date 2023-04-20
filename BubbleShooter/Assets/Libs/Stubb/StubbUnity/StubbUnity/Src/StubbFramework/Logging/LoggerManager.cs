using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace StubbUnity.StubbFramework.Logging
{
    public static class LoggerManager
    {
        private static LogLevel _globalLogLevel;
        private static Logger.LogDelegate _appenders;
        private static readonly Dictionary<string, Logger> Loggers = new Dictionary<string, Logger>();

        /// <summary>
        /// Set global log value.
        /// </summary>
        public static LogLevel GlobalLogLevel
        {
            get => _globalLogLevel;
            set
            {
                _globalLogLevel = value;
                foreach (var logger in Loggers.Values)
                {
                    logger.LogLevel = value;
                }
            }
        }

        /// <summary>
        /// Add appender to the loggers.
        /// </summary>
        /// <param name="appender"></param>
        public static void Add(Logger.LogDelegate appender)
        {
            _appenders += appender;
            foreach (var logger in Loggers.Values)
            {
                logger.OnLog += appender;
            }
        }

        /// <summary>
        /// Remove appender from the loggers.
        /// </summary>
        /// <param name="appender"></param>
        public static void Remove(Logger.LogDelegate appender)
        {
            _appenders -= appender;
            foreach (var logger in Loggers.Values)
            {
                logger.OnLog -= appender;
            }
        }

        /// <summary>
        /// Create new logger.
        /// </summary>
        /// <param name="name">Logger name</param>
        /// <returns></returns>
        public static Logger Create(string name)
        {
            if (Loggers.TryGetValue(name, out var logger)) return logger;
            
            logger = _CreateLogger(name);
            Loggers.Add(name, logger);

            return logger;
        }

        /// <summary>
        /// Clear all loggers and appenders.
        /// </summary>
        public static void Reset()
        {
            Loggers.Clear();
            _appenders = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Logger _CreateLogger(string name)
        {
            var logger = new Logger(name) {LogLevel = GlobalLogLevel};
            logger.OnLog += _appenders;
            return logger;
        }
    }
}