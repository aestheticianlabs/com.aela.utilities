using UnityEngine;
using UnityEngine.UI;

namespace AeLa.Utilities.UI
{
	// source: https://forum.unity.com/threads/content-size-fitter-refresh-problem.498536/#post-8066099
	public class RebuildUILayoutHelper : MonoBehaviour
	{
		public void RebuildLayout()
		{
			CoroutineUtils.Global.DoNextFrame(
				() => LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform)
			);
		}
	}
}