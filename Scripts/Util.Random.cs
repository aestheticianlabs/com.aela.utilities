using UnityEngine;
using URandom = UnityEngine.Random;

namespace AeLa.Utilities
{
	public partial class Util
	{
		public static class Random
		{
			private static readonly System.Random cachedRandom = new();

			public static Vector2Int Range(Vector2Int min, Vector2Int max)
			{
				return new(URandom.Range(min.x, max.x + 1), URandom.Range(min.y, max.y + 1));
			}

			/// <summary>
			/// Returns a random alphanumeric string of the provided length--similar to a hash
			/// </summary>
			/// <param name="length">The length of the string</param>
			public static string AlphanumericString(uint length)
			{
				const string characters = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

				var ret = string.Empty;

				for (int i = 0; i < length; i++)
				{
					ret += characters[cachedRandom.Next(characters.Length)];
				}

				return ret;
			}
		}
	}
}