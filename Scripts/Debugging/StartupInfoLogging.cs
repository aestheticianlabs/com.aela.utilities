#if !UNITY_EDITOR || AELA_STARTUP_INFO_EDITOR
using System.Globalization;
using System.Text;
using UnityEngine;

namespace AeLa.Utilities.Debugging
{
	/// <summary>
	/// Adds additional system/environment logging. Enable in editor with flag AELA_STARTUP_INFO_EDITOR.
	/// </summary>
	public static class StartupInfoLogging
	{
		[RuntimeInitializeOnLoadMethod]
		public static void Initialize()
		{
			var sb = new StringBuilder();
			sb.AppendLine("Runtime startup information:");
			
			sb.AppendLine($"Current culture: {CultureInfo.CurrentCulture}");
			// add more lines here as needed
			
			Debug.Log(sb.ToString());
		}
	}
}
#endif
