using System;
using System.Collections.Generic;
using UnityEngine;

namespace AeLa.Utilities
{
	public static class ComponentCache
	{
		private static readonly Dictionary<GameObject, Dictionary<Type, Component>> cache = new();
		private static readonly Dictionary<GameObject, Dictionary<Type, Component>> parentCache = new();

		[RuntimeInitializeOnLoadMethod]
		private static void ResetStatic()
		{
			cache.Clear();
			parentCache.Clear();
		}

		/// <summary>
		/// Returns the first component of the provided type on this GameObject or any of its parents.
		/// If a component of this type was previously found on any parents, that value will be returned instead.
		/// </summary>
		/// <remarks>
		/// Use this method with caution. Because the cached value will always be returned first, this method might
		/// return a different result than <see cref="GameObject.GetComponentInParent{T}"/> if the hierarchy has changed
		/// or a component has been added to a nearer parent.
		/// </remarks>
		public static T GetComponentInParentCached<T>(this GameObject go) where T : Component
		{
			return GetFromCache(go, parentCache, o => o.GetComponentInParent<T>());
		}

		/// <inheritdoc cref="GetComponentInParentCached{T}(GameObject)"/>
		public static T GetComponentInParentCached<T>(this Component component) where T : Component
		{
			return component.gameObject.GetComponentInParentCached<T>();
		}

		/// <inheritdoc cref="GetComponentInParentCached{T}(GameObject)"/>
		public static bool TryGetComponentInParentCached<T>(this GameObject go, out T component) where T : Component
		{
			component = go.GetComponentInParentCached<T>();
			return component;
		}

		/// <inheritdoc cref="GetComponentInParentCached{T}(GameObject)"/>
		public static bool TryGetComponentInParentCached<T>(this Component c, out T component) where T : Component
		{
			return c.gameObject.TryGetComponentInParentCached(out component);
		}

		public static T GetComponentCached<T>(this GameObject go) where T : Component
		{
			return GetFromCache(go, cache, o => o.GetComponent<T>());
		}

		public static T GetComponentCached<T>(this Component obj) where T : Component
			=> obj.gameObject.GetComponentCached<T>();

		public static bool TryGetComponentCached<T>(this GameObject go, out T component) where T : Component
		{
			return component = go.GetComponentCached<T>();
		}

		public static bool TryGetComponentCached<T>(this Component obj, out T component) where T : Component
			=> obj.gameObject.TryGetComponentCached(out component);

		public static T GetOrAddComponentCached<T>(this GameObject go) where T : Component
		{
			if (!go.TryGetComponentCached(out T component))
			{
				component = go.AddComponent<T>();
			}

			return component;
		}

		private static T GetFromCache<T>(
			GameObject go,
			Dictionary<GameObject, Dictionary<Type, Component>> cache,
			Func<GameObject, T> getter
		)
			where T : Component
		{
			// GameObject is being destroyed
			if (!go) return null;

			if (cache.TryGetValue(go, out var components) && components.TryGetValue(typeof(T), out var c))
			{
				if (c) return (T)c;

				// Component has been removed
				components.Remove(typeof(T));
			}

			var component = getter(go);
			if (!component) return null;

			if (components == null)
			{
				components = new();
				cache.Add(go, components);
				go.OnDestroy(o => cache.Remove(o));
			}

			components.Add(typeof(T), component);

			return component;
		}
	}
}