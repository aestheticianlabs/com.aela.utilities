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

		private void SendEvent(string eventName, Collider other)
		{
			foreach (var target in Targets)
			{
				target.SendMessage(eventName, other, RequireReceiver);
			}
		}
		
		private void OnTriggerEnter(Collider other)
		{
			if ((SendEvents & CollisionEventType.Enter) != CollisionEventType.Enter) return;

			SendEvent("OnTriggerEnter", other);
		}

		private void OnTriggerExit(Collider other)
		{
			if ((SendEvents & CollisionEventType.Exit) != CollisionEventType.Exit) return;

			SendEvent("OnTriggerExit", other);
		}
	}
}