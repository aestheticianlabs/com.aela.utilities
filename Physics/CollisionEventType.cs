using System;

namespace AeLa.Utilities.Physics
{
	[Flags]
	public enum CollisionEventType
	{
		Enter = 1,
		Exit = 1 << 1,
	}
}