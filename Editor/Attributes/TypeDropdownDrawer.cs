using System;
using System.Collections.Generic;
using System.Linq;
using AeLa.Utilities.Attributes;
using UnityEditor;
using UnityEngine;

namespace AeLa.Utilities.Editor.Attributes
{
	[CustomPropertyDrawer(typeof(TypeDropdownAttribute))]
	public class TypeDropdownDrawer : PropertyDrawer
	{
		private static readonly Dictionary<Type, List<Type>> objectTypes = new();

		private int selected = -1;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property == null) return;
			if (!(attribute is TypeDropdownAttribute typeDropdownAttribute)) return;

			if (property.propertyType != SerializedPropertyType.String)
			{
				Debug.LogError($"{nameof(TypeDropdownDrawer)} only supports string fields");
				return;
			}

			using (new EditorGUI.PropertyScope(position, label, property))
			{
				using (var changeCheck = new EditorGUI.ChangeCheckScope())
				{
					property.stringValue = TypeField(
						position, typeDropdownAttribute.Type, label,
						typeDropdownAttribute.AllowNone, property.stringValue,
						ref selected
					);
					if (changeCheck.changed)
					{
						property.serializedObject?.ApplyModifiedProperties();
					}
				}
			}
		}

		private static string TypeField(
			Rect position, Type type, GUIContent label, bool noneOption, string value, ref int selected
		)
		{
			if (!objectTypes.TryGetValue(type, out var typeList))
			{
				// Search through all of the assemblies to find any types that derive from specified type.
				typeList = new List<Type>();
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();
				for (int i = 0; i < assemblies.Length; ++i)
				{
					var assemblyTypes = assemblies[i].GetTypes();
					for (int j = 0; j < assemblyTypes.Length; ++j)
					{
						// Must derive from specified type.
						if (!type.IsAssignableFrom(assemblyTypes[j]))
						{
							continue;
						}

						// Ignore abstract classes.
						if (assemblyTypes[j].IsAbstract)
						{
							continue;
						}

						typeList.Add(assemblyTypes[j]);
					}
				}

				objectTypes.Add(type, typeList);
			}

			var options = new List<GUIContent>();
			if (noneOption)
			{
				options.Add(new GUIContent("None"));
			}

			options.AddRange(typeList.Select(t => new GUIContent(t.FullName)));

			if (selected == -1 && value == string.Empty && noneOption)
			{
				selected = 0;
			}
			else if (selected == -1)
			{
				selected = typeList.FindIndex(t => t.AssemblyQualifiedName == value);
				if (selected == -1)
				{
					Debug.LogError($"Could not find type {value}.");
				}

				if (noneOption)
				{
					selected += 1;
				}
			}

			selected = EditorGUI.Popup(position, label, selected, options.ToArray());

			if (noneOption)
			{
				return selected == 0 ? string.Empty : typeList[selected - 1].AssemblyQualifiedName;
			}

			return typeList[selected].AssemblyQualifiedName;
		}
	}
}