using UnityEngine;

namespace AeLa.Utilities.Physics
{
	/// <summary>
	/// Forwards OnCollision messages on this GameObject to the targets.
	/// </summary>
	public class ForwardCollisionEvents : MonoBehaviour
	{
		public GameObject[] Targets;
		public CollisionEventType SendEvents = (CollisionEventType)~0;
		public SendMessageOptions RequireReceiver = SendMessageOptions.DontRequireReceiver;

		private void SendEvent(string eventName, Collision collision)
		{
			foreach (var target in Targets)
			{
				target.SendMessage(eventName, collision, RequireReceiver);
			}
		}
		
		private void OnCollisionEnter(Collision collision)
		{
			if ((SendEvents & CollisionEventType.Enter) != CollisionEventType.Enter) return;

			SendEvent("OnCollisionEnter", collision);
		}

		private void OnCollisionExit(Collision collision)
		{
			if ((SendEvents & CollisionEventType.Exit) != CollisionEventType.Exit) return;

			SendEvent("OnCollisionExit", collision);
		}
	}
}