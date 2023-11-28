using System;
using System.Collections.Generic;
using UnityEngine;

namespace AeLa.Utilities
{
	public static class ComponentCache
	{
		private static Dictionary<GameObject, Dictionary<Type, Component>> cache = new();

		public static T GetComponentCached<T>(this GameObject go) where T : Component
		{
			// GameObject is being destroyed
			if (!go)
			{
				return null;
			}

			if (cache.TryGetValue(go, out var components) && components.TryGetValue(typeof(T), out var c))
			{

				// Component has been removed
				if (!c)
				{
					components.Remove(typeof(T));
					return null;
				}

				return (T)c;
			}

			if (!go.TryGetComponent(out T component)) return null;

			// add to cache if object has component
			if (components == null)
			{
				components = new();
				cache.Add(go, components);
			}

			components.Add(typeof(T), component);

			go.OnDestroy(RemoveFromCache);

			return component;
		}

		public static bool TryGetComponentCached<T>(this GameObject go, out T component) where T : Component
		{
			return component = go.GetComponentCached<T>();
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