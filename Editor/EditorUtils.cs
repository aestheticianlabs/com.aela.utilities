using UnityEditor;
using UnityEngine;

namespace AeLa.Utilities.Editor
{
	public static class EditorUtils
	{
		private const string MenuRoot = "Tools/Editor/";
		private const string PrefPrefix = "aela.utils.editor.";
		
		/// <summary>
		/// Deselects the currently selected GameObject(s) when playmode is entered.
		/// Helps avoid performance issues related to player, camera, etc. being accidentally selected.
		/// </summary>
		[InitializeOnLoad]
		public static class ClearSelectionOnPlay
		{
			private const string MenuPath = MenuRoot + "Clear Selection on Play";
			private const string PrefKey = PrefPrefix + "clearSelectOnPlay";
	
			public static bool Enabled
			{
				get => EditorPrefs.GetBool(PrefKey, true);
				set => EditorPrefs.SetBool(PrefKey, value);
			}
			
			static ClearSelectionOnPlay()
			{
				EditorApplication.playModeStateChanged += PlaymodeStateChanged;
			}
	
			private static void PlaymodeStateChanged(PlayModeStateChange change)
			{
				if (change != PlayModeStateChange.EnteredPlayMode) return;
				if (Selection.gameObjects.Length <= 0) return;
				
				Selection.activeGameObject = null;
			}
	
			[MenuItem(MenuPath)]
			private static void ToggleEnabled()
			{
				Enabled = !Enabled;
			}
	
			[MenuItem(MenuPath, true)]
			private static bool ValidateToggle()
			{
				Menu.SetChecked(MenuPath, Enabled);
				return true;
			}
		}

		[MenuItem(MenuRoot + "Open Application.persistentDataPath")]
		public static void OpenApplicationPersistentDataPath()
		{
			EditorUtility.RevealInFinder(Application.persistentDataPath + "/");
		}
	}
}