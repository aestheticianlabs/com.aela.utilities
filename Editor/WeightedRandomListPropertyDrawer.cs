using UnityEditor;
using UnityEngine;

namespace AeLa.Utilities.Editor
{
	[CustomPropertyDrawer(typeof(WeightedRandomList<>), true)]
	public class WeightedRandomListPropertyDrawer : PropertyDrawer
	{
		private const string ItemsProp = nameof(WeightedRandomList<object>.Items);

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property.FindPropertyRelative(ItemsProp), label);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property.FindPropertyRelative(ItemsProp), label);
		}
	}

	[CustomPropertyDrawer(typeof(WeightedRandomList<>.WeightedRandomItem))]
	public class WeightedRandomListElementPropertyDrawer : ForceExpandedPropertyDrawer
	{
		private const string ItemProp = nameof(WeightedRandomList<object>.WeightedRandomItem.Item);
		private const string WeightProp = nameof(WeightedRandomList<object>.WeightedRandomItem.Weight);

		public WeightedRandomListElementPropertyDrawer() : base(ItemProp, WeightProp)
		{
		}
	}
}
