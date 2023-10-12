using UnityEngine;

namespace AeLa.Utilities
{
	public class DisableOnPlatform : MonoBehaviour
	{
		public RuntimePlatformFlag Platforms;

		private void Awake()
		{
			if (Platforms.HasPlatform(Application.platform))
			{
				gameObject.SetActive(false);
			}
		}
	}
}