using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AeLa.Utilities.Editor
{
	public static class ContextMenuUtils
	{
		[MenuItem("CONTEXT/Transform/Remove all components")]
		public static void RemoveAllComponents()
		{
			const int attempts = 10;
			Undo.RecordObjects(Selection.gameObjects, "Remove all components");
			foreach (var go in Selection.gameObjects)
			{
				for (int i = 0; i < attempts; i++)
				{
					foreach (var c in go.GetComponents<Component>().Reverse())
					{
						if (c is Transform) continue;
	
						Object.DestroyImmediate(c);
					}

					if (go.GetComponents<Component>().Length <= 1)
					{
						break;
					}
				}
				
				EditorUtility.SetDirty(go);

				if (go.GetComponents<Component>().Length > 1)
				{
					Debug.LogWarning($"Failed to remove some components from {go.name}.");
				}
			}
		}
	}
}