using System.Collections.Generic;
using StubbUnity.StubbFramework.Logging;
using UnityEngine;
using Logger = StubbUnity.StubbFramework.Logging.Logger;

namespace StubbUnity.Unity.Logging
{
    public static class UnityLogAppender 
    {
        private delegate void UnityLogDelegate(string message);
        private static readonly Dictionary<LogLevel, UnityLogDelegate> Mapper;

        public static Logger.LogDelegate LogDelegate => Log;

        static UnityLogAppender()
        {
            Mapper = new Dictionary<LogLevel, UnityLogDelegate>
            {
                {LogLevel.Info, message => Debug.unityLogger.Log(LogType.Log, message)},
                {LogLevel.Warn, message => Debug.unityLogger.Log(LogType.Warning, message)},
                {LogLevel.Error, message => Debug.unityLogger.Log(LogType.Error, message)},
                {LogLevel.Fatal, message => Debug.unityLogger.Log(LogType.Exception, message)},
                {LogLevel.Trace, message => Debug.unityLogger.Log(LogType.Log, message)},
                {LogLevel.Debug, message => Debug.unityLogger.Log(LogType.Log, message)}
            };
        }
	
        private static void Log(Logger logger, LogLevel logLevel, string message)
        {
            Mapper[logLevel].Invoke(message);
        }
    }
}