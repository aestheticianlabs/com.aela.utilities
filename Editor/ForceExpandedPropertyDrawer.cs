using UnityEditor;
using UnityEngine;

namespace AeLa.Utilities.Editor
{
	public abstract class ForceExpandedPropertyDrawer : PropertyDrawer
	{
		private string[] props;
		protected virtual bool showLabel => false;

		private static readonly float fullLineHeight =
			EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

		private static readonly float padding = 3f;

		protected ForceExpandedPropertyDrawer(params string[] props)
		{
			this.props = props;
		}

		public override void OnGUI(
			Rect position,
			SerializedProperty property,
			GUIContent label
		)
		{
			EditorGUI.BeginProperty(position, label, property);

			if (showLabel)
			{
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			}

			// Calculate rects
			var rect = new Rect(position.x, position.y, position.width, fullLineHeight);
			foreach (var prop in props)
			{
				var p = property.FindPropertyRelative(prop);
				rect.height = EditorGUI.GetPropertyHeight(p, true);
				EditorGUI.PropertyField(rect, p, true);
				rect.y += rect.height + EditorGUIUtility.standardVerticalSpacing;
			}

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var height = padding;

			foreach (var prop in props)
			{
				height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative(prop), true) +
				          EditorGUIUtility.standardVerticalSpacing;
			}

			return height;
		}
	}
}