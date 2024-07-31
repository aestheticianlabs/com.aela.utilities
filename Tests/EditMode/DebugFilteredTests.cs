using System;
using System.Text.RegularExpressions;
using AeLa.Utilities.Debugging;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AeLa.Utilities.Tests.EditMode.Tests.EditMode
{
	public class DebugFilteredTests
	{
		// todo: test with all levels

		[Test]
		public void LogException_LogLevelInfo_MessageIsLogged()
		{
			var e = new Exception("Test exception!");
			LogAssert.Expect(LogType.Exception, new Regex(e.Message, RegexOptions.CultureInvariant));
			DebugFiltered.LogException(LogLevel.Info, e);
		}

		[Test]
		public void LogException_LogLevelNone_MessageNotLogged()
		{
			var e = new Exception("Test exception!");
			DebugFiltered.LogException(LogLevel.None, e);
		}
	}
}