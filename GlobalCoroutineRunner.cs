using UnityEngine;

namespace AeLa.Utilities
{
	public class GlobalCoroutineRunner : MonoBehaviour
	{
		private static GlobalCoroutineRunner instance;
		public static GlobalCoroutineRunner Instance
		{
			get
			{
				if (!instance)
				{
					var go = new GameObject
					{
						name = "[ Global Coroutines ]",
						hideFlags = HideFlags.HideAndDontSave
					};
					instance = go.AddComponent<GlobalCoroutineRunner>();
					DontDestroyOnLoad(instance.gameObject);
				}

				return instance;
			}
		}
	}
}