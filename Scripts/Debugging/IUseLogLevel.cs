using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AeLa.Utilities.Debugging
{
	public interface IUseLogLevel
	{
		public LogLevel LogLevel { get; set; }
	}

	public static class IUseLogLevelExtensions
	{
        [HideInCallstack]
		public static void Log(this IUseLogLevel source, LogType logType, object message, Object context = null)
		{
			DebugFiltered.Log(logType, source.LogLevel, message, context);
		}

        [HideInCallstack]
		public static void Log(this IUseLogLevel source, object message, Object context = null)
		{
			DebugFiltered.Log(source.LogLevel, message, context);
		}

        [HideInCallstack]
		public static void LogWarning(this IUseLogLevel source, object message, Object context = null)
		{
			DebugFiltered.LogWarning(source.LogLevel, message, context);
		}

        [HideInCallstack]
		public static void LogError(this IUseLogLevel source, object message, Object context = null)
		{
			DebugFiltered.LogError(source.LogLevel, message, context);
		}

        [HideInCallstack]
		public static void LogException(this IUseLogLevel source, Exception exception, Object context = null)
		{
			DebugFiltered.LogException(source.LogLevel, exception, context);
		}
	}
}