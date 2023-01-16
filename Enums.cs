using System;

namespace AeLa.Utilities
{
	[Flags]
	public enum StartupPhase
	{
		Awake = 1 << 0,
		OnEnable = 1 << 1,
		Start = 1 << 2
	}
}