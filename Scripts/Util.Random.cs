using UnityEngine;
using URandom = UnityEngine.Random;

namespace AeLa.Utilities
{
	public partial class Util
	{
		public static class Random
		{
			public static Vector2Int Range(Vector2Int min, Vector2Int max)
			{
				return new(URandom.Range(min.x, max.x + 1), URandom.Range(min.y, max.y + 1));
			}
		}
	}
}