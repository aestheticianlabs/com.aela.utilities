using System;
using UnityEngine;

namespace AeLa.Utilities.Attributes
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class TypeDropdownAttribute : PropertyAttribute
	{
		public readonly Type Type;
		public readonly bool AllowNone;

		public TypeDropdownAttribute(Type type, bool allowNone = true)
		{
			Type = type;
			AllowNone = allowNone;
		}
	}
}