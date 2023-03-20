using System.Collections.Generic;
using UnityEngine;

namespace AeLa.Utilities.Physics
{
	/// <summary>
	/// Forwards OnTrigger messages on this GameObject to the targets.
	/// </summary>
	public class ForwardTriggerEvents : MonoBehaviour
	{
		public GameObject[] Targets;
		public CollisionEventType SendEvents = (CollisionEventType)~0;
		public SendMessageOptions RequireReceiver = SendMessageOptions.DontRequireReceiver;

		/// <summary>
		/// Send Exit events when this GameObject/component is disabled
		/// </summary>
		[Tooltip("Send Exit events when this GameObject/component is disabled")]
		public bool ExitOnDisable = true;

		private List<Collider> collidersEntered = new();

		private void SendEvent(string eventName, Collider other)
		{
			foreach (var target in Targets)
			{
				target.SendMessage(eventName, other, RequireReceiver);
			}
		}

		private void OnDisable()
		{
			if (!ExitOnDisable || (SendEvents & CollisionEventType.Exit) != CollisionEventType.Exit) return;
			
			foreach (var collider in collidersEntered)
			{
				SendEvent("OnTriggerExit", collider);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((SendEvents & CollisionEventType.Enter) != CollisionEventType.Enter) return;

			collidersEntered.Add(other);
			SendEvent("OnTriggerEnter", other);
		}

		private void OnTriggerExit(Collider other)
		{
			if ((SendEvents & CollisionEventType.Exit) != CollisionEventType.Exit) return;

			collidersEntered.Remove(other);
			SendEvent("OnTriggerExit", other);
		}
	}
}