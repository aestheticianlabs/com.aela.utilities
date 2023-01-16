using AeLa.KA.Utility;
using UnityEditor;
using UnityEngine;

namespace AeLa.Utilities.Editor
{
	[CustomPropertyDrawer(typeof(WeightedRandomList<>))]
	public class WeightedRandomListPropertyDrawer : PropertyDrawer 
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property.FindPropertyRelative("items"), label);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("items"), label);
		}
	}

	[CustomPropertyDrawer(typeof(WeightedRandomList<>.WeightedRandomItem))]
	public class WeightedRandomListElementPropertyDrawer : ForceExpandedPropertyDrawer
	{
		public WeightedRandomListElementPropertyDrawer() : base("Item", "Weight") { }
	}
}