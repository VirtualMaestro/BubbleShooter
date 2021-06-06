using System;
using System.Runtime.CompilerServices;

namespace StubbUnity.StubbFramework.Logging {
    public class Logger {
        public delegate void LogDelegate(Logger logger, LogLevel logLevel, string message);

        public event LogDelegate OnLog;

        public LogLevel LogLevel { get; set; }
        public string Name { get; }

        public Logger(string name) {
            Name = name;
        }

        public void Trace(string message) {
            _Log(LogLevel.Trace, message);
        }

        public void Debug(string message) {
            _Log(LogLevel.Debug, message);
        }

        public void Info(string message) {
            _Log(LogLevel.Info, message);
        }

        public void Warn(string message) {
            _Log(LogLevel.Warn, message);
        }

        public void Error(string message) {
            _Log(LogLevel.Error, message);
        }

        public void Fatal(string message) {
            _Log(LogLevel.Fatal, message);
        }

        public void Assert(bool condition, string message) {
            if (!condition) {
                throw new Exception(message);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _Log(LogLevel logLevel, string message)
        {
            if (LogLevel > logLevel) return;
            OnLog?.Invoke(this, logLevel, message);
        }
    }
}

