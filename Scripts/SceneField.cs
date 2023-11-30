// Source: http://answers.unity.com/comments/1374414/view.html

using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AeLa.Utilities
{
	/// <summary>
	/// Unity editor-friendly scene reference field.
	/// </summary>
	[System.Serializable]
	public class SceneField
	{
		[SerializeField] private Object sceneAsset;
		[SerializeField] private string sceneName = "";

		/// <summary>
		/// The name of the scene.
		/// </summary>
		public string SceneName => sceneName;

		// makes it work with the existing Unity methods (LoadLevel/LoadScene)
		public static implicit operator string(SceneField sceneField)
		{
			return sceneField.SceneName;
		}

		[Conditional("UNITY_EDITOR")]
		public void RefreshSceneName()
		{
#if UNITY_EDITOR
			sceneName = Path.GetFileNameWithoutExtension(
				path: AssetDatabase.GetAssetPath(sceneAsset)
			);
#endif
		}
	}

#if UNITY_EDITOR
	/// <summary>
	/// Provides a <see cref="PropertyDrawer"/> for <see cref="SceneField"/>.
	/// </summary>
	[CustomPropertyDrawer(typeof(SceneField))]
	public class SceneFieldPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(
			Rect position,
			SerializedProperty property,
			GUIContent label
		)
		{
			EditorGUI.BeginProperty(position, GUIContent.none, property);
			var sceneAsset = property.FindPropertyRelative("sceneAsset");
			var sceneName = property.FindPropertyRelative("sceneName");

			position = EditorGUI.PrefixLabel(
				totalPosition: position,
				id: GUIUtility.GetControlID(FocusType.Passive),
				label: label
			);

			if (sceneAsset != null)
			{
				// warn if scene isn't in build settings
				string scenePath = AssetDatabase.GetAssetPath(
					assetObject: sceneAsset.objectReferenceValue
				);

				EditorGUI.BeginChangeCheck();

				var value = sceneAsset.objectReferenceValue;
				value = EditorGUI.ObjectField(
					position: position,
					obj: value,
					objType: typeof(SceneAsset),
					allowSceneObjects: false
				);

				if (
					value &&
					(EditorGUI.EndChangeCheck() ||
					value.name != sceneName.stringValue)
				)
				{
					sceneAsset.objectReferenceValue = value;
					if (sceneAsset.objectReferenceValue != null)
					{
						scenePath = AssetDatabase.GetAssetPath(
							sceneAsset.objectReferenceValue
						);
						sceneName.stringValue = Path.GetFileNameWithoutExtension(
							path: scenePath
						);
					}
					else
					{
						sceneName.stringValue = null;
					}
				}

				// name label
				EditorGUI.BeginDisabledGroup(true);
				var style = new GUIStyle(EditorStyles.label);
				if (!EditorBuildSettings.scenes.Any(s => s.path == scenePath))
				{
					style.normal.textColor = Color.red;
				}
				EditorGUI.LabelField(position, null, sceneName.stringValue, style);
				EditorGUI.EndDisabledGroup();
			}
			EditorGUI.EndProperty();
		}
	}
#endif
}