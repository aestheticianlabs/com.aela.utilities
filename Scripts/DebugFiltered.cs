using System;
using AeLa.Utilities.Debugging;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AeLa.Utilities
{
	[PublicAPI]
	public static class DebugFiltered
	{
        [HideInCallstack]
		public static void Log(LogType logType, LogLevel filterLevel, object message, Object context = null)
		{
			if (LogTypeToLevel(logType) > filterLevel) return;
			Debug.unityLogger.Log(logType, message, context);
		}

        [HideInCallstack]
		public static void Log(LogLevel filterLevel, object message, Object context = null) =>
			Log(LogType.Log, filterLevel, message, context);

        [HideInCallstack]
		public static void LogWarning(LogLevel filterLevel, object message, Object context = null) =>
			Log(LogType.Warning, filterLevel, message, context);

        [HideInCallstack]
		public static void LogError(LogLevel filterLevel, object message, Object context = null) =>
			Log(LogType.Error, filterLevel, message, context);

        [HideInCallstack]
		public static void LogException(LogLevel filterLevel, Exception exception, Object context = null)
		{
			if (LogTypeToLevel(LogType.Exception) > filterLevel) return;
			Debug.unityLogger.LogException(exception, context);
		}

		private static LogLevel LogTypeToLevel(LogType logType)
		{
			return logType switch
			{
				LogType.Exception => LogLevel.Exception,
				LogType.Error => LogLevel.Error,
				LogType.Assert => LogLevel.Error,
				LogType.Warning => LogLevel.Warning,
				LogType.Log => LogLevel.Info,
				_ => throw new ArgumentOutOfRangeException(nameof(logType), logType, null)
			};
		}
	}
}