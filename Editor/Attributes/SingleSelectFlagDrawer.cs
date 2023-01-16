using System;
using AeLa.Utilities.Attributes;
using UnityEditor;
using UnityEngine;

namespace AeLa.Utilities.Editor.Attributes
{
	[CustomPropertyDrawer(typeof(SingleSelectFlagAttribute))]
	public class SingleSelectFlagDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var type = fieldInfo.FieldType;
			if (!type.IsEnum)
			{
				Debug.LogError(
					$"{nameof(SingleSelectFlagAttribute)} is only valid on enum types. Invalid property usage: {fieldInfo.Name}"
				);
				return;
			}


			property.intValue = Convert.ToInt32(
				EditorGUI.EnumPopup(position, label, (Enum)Enum.ToObject(type, property.intValue))
			);
		}
	}
}