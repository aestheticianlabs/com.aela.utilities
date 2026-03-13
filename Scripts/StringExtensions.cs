using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;

namespace AeLa.Utilities
{
	[PublicAPI]
	public static class StringExtensions
	{
		/// <summary>
		/// Wraps text in a rich text color tag
		/// </summary>
		public static string Color(this string text, Color color) =>
			$"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";

		/// <summary>
		/// Wraps text in a rich text bold tag
		/// </summary>
		public static string Bold(this string text) => $"<b>{text}</b>";

		/// <summary>
		/// Wraps text in a rich text italic tag
		/// </summary>
		public static string Italic(this string text) => $"<i>{text}</i>";

		/// <summary>
		/// Wraps text in a rich text size tag
		/// </summary>
		public static string Size(this string text, int size) => $"<size={size}>{text}</size>";

		static readonly Regex whitespaceRegex = new(@"\s+", RegexOptions.Compiled);

		public static string ReplaceWhiteSpaces(this string text, string replacement = "")
		{
			return whitespaceRegex.Replace(text, replacement);
		}
	}
}