using UnityEngine;

namespace AeLa.Utilities
{
	public class WaitForFrames : CustomYieldInstruction
	{
		private int start;
		private int delay;
		
		public override bool keepWaiting => Time.frameCount - start < delay;

		public WaitForFrames(int frameCount)
		{
			start = Time.frameCount;
			delay = frameCount;
		}
	}
}