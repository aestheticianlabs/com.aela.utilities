using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AeLa.Utilities.Physics
{
	[PublicAPI]
	public static class PhysicsEventExtensions
	{
		public static GameObject AddTriggerListener(
			this GameObject go,
			Action<GameObject, Collider> enter = null,
			Action<GameObject, Collider> exit = null
		)
		{
			var component = go.GetOrAddComponentCached<TriggerEvents>();

			if (enter != null) component.TriggerEnter += enter;
			if (exit != null) component.TriggerEnter += exit;

			return go;
		}

		public static GameObject RemoveTriggerListener(
			this GameObject go,
			Action<GameObject, Collider> enter = null,
			Action<GameObject, Collider> exit = null
		)
		{
			if (go.TryGetComponentCached(out TriggerEvents component))
			{
				if (enter != null) component.TriggerEnter -= enter;
				if (exit != null) component.TriggerExit -= exit;
			}
			else
			{
				Debug.LogWarning(
					$"Can not remove trigger events for {go} because it is missing the {typeof(TriggerEvents)} component"
				);
			}

			return go;
		}

		/// <summary>
		/// Allows code to subscribe to an object's TriggerEnter/Exit events
		/// </summary>
		private class TriggerEvents : MonoBehaviour
		{
			public event Action<GameObject, Collider> TriggerEnter;
			public event Action<GameObject, Collider> TriggerExit;

			private void OnTriggerEnter(Collider other)
			{
				TriggerEnter?.Invoke(gameObject, other);
			}

			private void OnTriggerExit(Collider other)
			{
				TriggerExit?.Invoke(gameObject, other);
			}
		}
	}
}