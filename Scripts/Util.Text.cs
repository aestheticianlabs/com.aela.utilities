using JetBrains.Annotations;
using UnityEngine;

namespace AeLa.Utilities
{
	public static partial class Util
	{
		[PublicAPI]
		public static class Text
		{
			/// <summary>
			/// Returns the sign for the provided number. + if >=0, blank if < 0 (because it already has -)
			/// </summary>
			public static string GetSignSymbol(int value) => value >= 0 ? "+" : "";

			public static string ToStringWithSign(int value, string formatString = "{0}{1}") =>
				string.Format(formatString, GetSignSymbol(value), value);

			public static string ToStringWithSignAndColor(
				int value, Color positiveColor, Color negativeColor, string formatString = "{0}{1}"
			)
			{
				return string.Format(formatString, GetSignSymbol(value), value).Color(
					value >= 0 ? positiveColor : negativeColor
				);
			}

			public static string ToStringWithSignAndColor(int value, string formatString = "{0}{1}") =>
				ToStringWithSignAndColor(value, Color.green, Color.red, formatString);
		}
	}
}