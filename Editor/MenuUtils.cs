using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AeLa.Utilities.Editor
{
	public static class MenuUtils
	{
		[MenuItem("GameObject/Select Modified Prefabs in Scene")]
		public static void SelectModifiedPrefabs()
		{
			var scene = EditorSceneManager.GetActiveScene();
			Selection.objects = scene.GetRootGameObjects()
				.Where(PrefabUtility.IsOutermostPrefabInstanceRoot)
				.Where(go => PrefabUtility.GetObjectOverrides(go).Count(IsNonTrivialOverride) > 0)
				.ToArray();
		}

		private static bool IsNonTrivialOverride(ObjectOverride o)
		{
			var t = o.instanceObject.GetType();
			return !typeof(Transform).IsAssignableFrom(t) && !typeof(GameObject).IsAssignableFrom(t);
		}
	}
}