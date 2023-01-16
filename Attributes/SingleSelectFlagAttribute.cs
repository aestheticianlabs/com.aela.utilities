using System;
using UnityEngine;

namespace AeLa.Utilities.Attributes
{
	/// <summary>
	/// Draws an enum with <see cref="FlagsAttribute"/> as a single-item select dropdown in the editor.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class SingleSelectFlagAttribute : PropertyAttribute
	{
	}
}