using System.Diagnostics;

namespace StubbUnity.StubbFramework.Logging
{
    public static class log
    {
        private static readonly Logger _log;

        static log()
        {
            _log = LoggerManager.Create("StubbLogger");
        }

        [Conditional("DEBUG")]
        public static void Trace(string message)
        {
            _log.Trace(message);
        }

        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            _log.Debug(message);
        }

        [Conditional("DEBUG")]
        public static void Info(string message)
        {
            _log.Info(message);
        }

        [Conditional("DEBUG")]
        public static void Warn(string message)
        {
            _log.Warn(message);
        }

        [Conditional("DEBUG")]
        public static void Error(string message)
        {
            _log.Error(message);
        }

        [Conditional("DEBUG")]
        public static void Fatal(string message)
        {
            _log.Fatal(message);
        }

        [Conditional("DEBUG")]
        public static void Assert(bool condition, string message)
        {
            _log.Assert(condition, message);
        }

        public static void AddAppender(Logger.LogDelegate appender)
        {
            LoggerManager.Add(appender);
        }
    }
}