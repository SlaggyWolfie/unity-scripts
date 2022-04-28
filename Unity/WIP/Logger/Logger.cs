using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Slaggy
{
    // TODO: Make it port to wherever, also less ugly messages
    public class Logger
    {
        public enum LogType { Error, Assert, Warning, Log, Exception, }

        public static void Log(LogType logType, object message, object sender)
        {
#if UNITY_5_3_OR_NEWER
            UnityEngine.LogType lt = (UnityEngine.LogType) logType;
            Debug.unityLogger.Log(lt, message, sender as UnityEngine.Object);
#else
            Console.WriteLine($"[{logType.ToString().ToUpperInvariant()}] {sender}: {message}");
#endif
        }

        public static void Log(LogType logType, object message) => Log(logType, message, null);

        public static void Log(object message) => Log(LogType.Log, message);
        public static void Log(object message, object sender) => Log(LogType.Log, message, sender);

        public static void LogWarning(object message) => Log(LogType.Warning, message);
        public static void LogWarning(object message, object sender) => Log(LogType.Warning, message, sender);

        public static void LogError(object message) => Log(LogType.Error, message);
        public static void LogError(object message, object sender) => Log(LogType.Error, message, sender);

        public static void LogException(Exception exception) =>
            Log(LogType.Exception, exception.Message + '\n' + exception.StackTrace);
        public static void LogException(Exception exception, object sender) =>
            Log(LogType.Exception, exception.Message + '\n' + exception.StackTrace, sender);
    }
}
