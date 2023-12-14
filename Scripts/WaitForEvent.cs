using UnityEngine;
using System;

namespace AeLa.Utilities
{
	/// <summary>
	/// Waits until the provided Action is called
	/// </summary>
	public class WaitForEvent : CustomYieldInstruction
	{
		public override bool keepWaiting => !called;

		private Action callback;
		private bool called;

		public WaitForEvent(Action callback)
		{
			callback += Wait;
			this.callback = callback;
		}

		private void Wait()
		{
			called = true;
			callback -= Wait;
		}
	}
}