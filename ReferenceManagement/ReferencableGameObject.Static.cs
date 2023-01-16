using System.Collections.Generic;
using UnityEngine;

namespace AeLa.Utilities.ReferenceManagement
{
	public partial class ReferencableGameObject
	{
		private static readonly Dictionary<GameObjectReference, List<GameObject>> references =
			new Dictionary<GameObjectReference, List<GameObject>>();

		private static readonly Dictionary<string, GameObjectReference> keys =
			new Dictionary<string, GameObjectReference>();

		public static void InitializeReference(GameObjectReference reference)
		{
			if (!string.IsNullOrWhiteSpace(reference.Key) && !keys.ContainsKey(reference.Key))
			{
				keys.Add(reference.Key, reference);
			}
		}

		public static void OnReferenceDestroy(GameObjectReference reference)
		{
			if (!string.IsNullOrWhiteSpace(reference.Key) && keys.ContainsKey(reference.Key))
			{
				keys.Remove(reference.Key);
			}
		}

		public static GameObjectReference GetReference(string key)
		{
			return !keys.ContainsKey(key) ? null : keys[key];
		}

		public static bool TryGetReference(string key, out GameObjectReference reference)
		{
			return reference = GetReference(key);
		}

		public static GameObject FindSingle(GameObjectReference reference)
		{
			return references.TryGetValue(reference, out var refs) && refs.Count > 0
				? refs[0]
				: null;
		}

		public static GameObject FindSingle(string key)
		{
			return !TryGetReference(key, out var reference) ? null : FindSingle(reference);
		}

		public static bool TryFindSingle(GameObjectReference reference, out GameObject go)
		{
			return go = FindSingle(reference);
		}

		public static bool TryFindSingle(string key, out GameObject go)
		{
			return go = FindSingle(key);
		}

		public static T FindSingle<T>(GameObjectReference reference) where T : Object
		{
			return TryFindSingle(reference, out var go) ? go.GetComponent<T>() : null;
		}

		public static T FindSingle<T>(string key) where T : Object
		{
			return TryFindSingle(key, out var go) ? go.GetComponent<T>() : null;
		}

		public static bool TryFindSingle<T>(GameObjectReference reference, out T o) where T : Object
		{
			return o = FindSingle<T>(reference);
		}

		public static bool TryFindSingle<T>(string key, out T o) where T : Object
		{
			return o = FindSingle<T>(key);
		}

		public static List<GameObject> FindAll(GameObjectReference reference)
		{
			if (reference.IsUnique)
			{
				Debug.LogWarning(
					$"Using FindAll to find unique reference {reference} will always return a list with up to one object."
				);
			}

			return references.TryGetValue(reference, out var refs) ? refs : null;
		}

		public static List<GameObject> FindAll(string key)
		{
			var reference = GetReference(key);
			
			if (reference.IsUnique)
			{
				Debug.LogWarning(
					$"Using FindAll to find unique reference {reference} will always return a list with up to one object."
				);
			}

			return references.TryGetValue(reference, out var refs) ? refs : null;
		}

		public static bool TryFindAll(GameObjectReference reference, out List<GameObject> objects)
		{
			objects = FindAll(reference);
			return objects?.Count > 0;
		}

		public static bool TryFindAll(string key, out List<GameObject> objects)
		{
			objects = FindAll(key);
			return objects?.Count > 0;
		}

		public static List<T> FindAll<T>(GameObjectReference reference) where T : Object
		{
			if (!TryFindAll(reference, out var all) || all.Count == 0) return null;

			var match = new List<T>();
			foreach (var go in all)
			{
				if (go.TryGetComponent(out T component))
					match.Add(component);
			}

			return match;
		}

		public static List<T> FindAll<T>(string key) where T : Object
		{
			return !TryGetReference(key, out var reference) ? null : FindAll<T>(reference);
		}
	}
}