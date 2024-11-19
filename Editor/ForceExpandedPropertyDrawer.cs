using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AeLa.Utilities.Editor
{
	public abstract class ForceExpandedPropertyDrawer : PropertyDrawer
	{
		private string[] props;
		protected virtual bool ShowLabel => false;

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

			if (ShowLabel)
			{
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			}

			// Calculate rects
			var rect = new Rect(position.x, position.y, position.width, fullLineHeight);
			foreach (var prop in Enumerate(property))
			{
				rect.height = EditorGUI.GetPropertyHeight(prop, true);
				EditorGUI.PropertyField(rect, prop, true);
				rect.y += rect.height + EditorGUIUtility.standardVerticalSpacing;
			}

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return padding + Enumerate(property).Sum(
				prop => EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing
			);
		}

		protected IEnumerable<SerializedProperty> Enumerate(SerializedProperty property)
		{
			if (props?.Length > 0)
			{
				return props.Select(property.FindPropertyRelative);
			}

			return EnumerateProps(property);
		}

		protected IEnumerable<SerializedProperty> EnumerateProps(SerializedProperty property)
		{
			var e = property.GetEnumerator();
			var rootDepth = property.depth;
			using (e as IDisposable)
			{
				while (e.MoveNext())
				{
					var prop = (SerializedProperty)e.Current;
					if (prop?.depth > rootDepth + 1) continue;
					yield return prop;
				}
			}
		}
	}
}