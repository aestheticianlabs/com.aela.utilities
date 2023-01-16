using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AeLa.Utilities
{
	public class DebugSceneReloader : MonoBehaviour
	{
		[SerializeField] private KeyCode key = KeyCode.R;
		[SerializeField] private KeyCode modifier = KeyCode.LeftControl;

		private void Update()
		{
			if (modifier == KeyCode.None || Input.GetKey(modifier) && Input.GetKeyDown(key))
			{
				var activeScene = SceneManager.GetActiveScene();
#if UNITY_EDITOR
				// reload in editor for scenes not in the build
				if (!activeScene.IsValid())
				{
					UnityEditor.SceneManagement.EditorSceneManager.LoadSceneInPlayMode(
						UnityEditor.AssetDatabase.GUIDToAssetPath(
							UnityEditor.AssetDatabase.FindAssets(
								$"t:SceneAsset {activeScene.name}",
								new string[] { "Assets" }
							)[0]
						),
						new LoadSceneParameters
						{
							loadSceneMode = LoadSceneMode.Single
						}
					);
					return;
				}
#endif
				SceneManager.LoadScene(activeScene.buildIndex);
			}
		}

		private void OnGUI()
		{
			GUILayout.Label("Press " + (modifier != KeyCode.None ? $"{modifier} + " : String.Empty) + $"{key} to reload scene");
		}
	}
}