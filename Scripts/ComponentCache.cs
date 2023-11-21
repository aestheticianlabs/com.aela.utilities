using System;
using System.Collections.Generic;
using UnityEngine;

namespace AeLa.Utilities
{
	public static class ComponentCache
	{
		private static Dictionary<GameObject, Dictionary<Type, Component>> cache = new();

		public static bool TryGetComponentCached<T>(this GameObject go, out T component) where T : Component
		{
			// GameObject is being destroyed
			if (!go)
			{
				component = null;
				return false;
			}

			if (cache.TryGetValue(go, out var components) && components.TryGetValue(typeof(T), out var c))
			{

				// Component has been removed
				if (!c)
				{
					components.Remove(typeof(T));
					component = null;
					return false;
				}

				component = (T)c;
				return true;
			}

			if (!go.TryGetComponent(out component)) return false;

			// add to cache if object has component
			if (components == null)
			{
				components = new();
				cache.Add(go, components);
			}

			components.Add(typeof(T), component);

			go.OnDestroy(RemoveFromCache);

			return true;
		}

		public static T GetOrAddComponentCached<T>(this GameObject go)  where T : Component
		{
			if (!go.TryGetComponentCached(out T component))
			{
				component = go.AddComponent<T>();
			}

			return component;
		}

		private static void RemoveFromCache(GameObject go)
		{
			cache.Remove(go);
		}
	}
}