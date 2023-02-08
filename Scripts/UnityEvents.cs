using System;
using UnityEngine;
using UnityEngine.Events;

namespace AeLa.Utilities
{
	[Serializable]
	public class GameObjectUnityEvent : UnityEvent<GameObject> {}
	
	[Serializable]
	public class BoolUnityEvent : UnityEvent<bool> {}
}