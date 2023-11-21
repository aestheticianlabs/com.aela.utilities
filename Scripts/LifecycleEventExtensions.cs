using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AeLa.Utilities
{
	[PublicAPI]
	public static class LifecycleEventExtensions
	{
		/// <summary>
		/// Invokes the provided callback on this object's OnDestroy event
		/// </summary>
		public static GameObject OnDestroy(this GameObject go, Action<GameObject> callback) =>
			Add<OnDestroyComponent>(go, callback);

		/// <summary>
		/// Stops listening to this object's OnDestroy event for the provided callback
		/// </summary>
		public static GameObject RemoveOnDestroyCallback(this GameObject go, Action<GameObject> callback) =>
			Remove<OnDestroyComponent>(go, callback);

		/// <summary>
		/// Invokes the provided callback on this object's OnEnable event
		/// </summary>
		public static GameObject OnEnable(this GameObject go, Action<GameObject> callback) =>
			Add<OnEnableComponent>(go, callback);

		/// <summary>
		/// Stops listening to this object's OnEnable event for the provided callback
		/// </summary>
		public static GameObject RemoveOnEnableCallback(this GameObject go, Action<GameObject> callback) =>
			Remove<OnEnableComponent>(go, callback);


		/// <summary>
		/// Invokes the provided callback on this object's OnDisable event
		/// </summary>
		public static GameObject OnDisable(this GameObject go, Action<GameObject> callback) =>
			Add<OnDisableComponent>(go, callback);

		/// <summary>
		/// Stops listening to this object's OnDisable event for the provided callback
		/// </summary>
		public static GameObject RemoveOnDisableCallback(this GameObject go, Action<GameObject> callback) =>
			Remove<OnDisableComponent>(go, callback);

		private static GameObject Add<T>(GameObject go, Action<GameObject> callback)
			where T : LifecycleEventComponent
		{
			var component = go.GetOrAddComponentCached<T>();
			component.Callback += callback;
			return go;
		}

		private static GameObject Remove<T>(GameObject go, Action<GameObject> callback)
			where T : LifecycleEventComponent
		{
			if (go.TryGetComponentCached(out T component))
			{
				component.Callback -= callback;
			}
			else
			{
				Debug.LogWarning($"Can't remove event listener because {go} is missing the {typeof(T)} component");
			}

			return go;
		}

		private abstract class LifecycleEventComponent : MonoBehaviour
		{
			public event Action<GameObject> Callback;

			protected virtual void InvokeCallback()
			{
				Callback?.Invoke(gameObject);
			}
		}

		private class OnDestroyComponent : LifecycleEventComponent
		{
			private void OnDestroy() => InvokeCallback();
		}

		private class OnEnableComponent : LifecycleEventComponent
		{
			private void OnEnable() => InvokeCallback();
		}

		private class OnDisableComponent : LifecycleEventComponent
		{
			private void OnDisable() => InvokeCallback();
		}
	}
}