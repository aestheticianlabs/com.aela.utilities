using UnityEngine;

namespace AeLa.Utilities
{
	public static partial class Util
	{
		public static class Math
		{
			public const float TAU = 2 * Mathf.PI;

			/// <summary>
			/// Returns the side of <paramref name="pos"/> relative to 
			/// <paramref name="rel"/> along <paramref name="along"/>
			/// </summary>
			public static float SideOf(Vector3 pos, Vector3 rel, Vector3 along) =>
				SideOf(pos - rel, along);

			/// <summary>
			/// Returns the side of <paramref name="rel"/>
			/// along <paramref name="along"/>
			/// </summary>
			public static float SideOf(Vector3 rel, Vector3 along) =>
				Mathf.Sign(Vector3.Dot(rel, along));

			/// <summary>
			/// Returns a vector with component values clamped to min and max
			/// </summary>
			/// <param name="v">The value to be clamped</param>
			/// <param name="min">The minimum values</param>
			/// <param name="max">The maximum values</param>
			/// <param name="origin">The relative origin to use when clamping. Subtracted from <see cref="v"/> before clamping and added before clamped value is returned.</param>
			public static Vector3 ClampComponents(Vector3 v, Vector3 min, Vector3 max, Vector3 origin = default)
			{
				v -= origin;
				return new Vector3
				{
					x = Mathf.Clamp(v.x, min.x, max.x),
					y = Mathf.Clamp(v.y, min.y, max.y),
					z = Mathf.Clamp(v.z, min.z, max.z)
				} + origin;
			}

			/// <summary>
			/// Returns a vector with component values clamped to min and max
			/// </summary>
			/// <param name="v">The value to be clamped</param>
			/// <param name="min">The minimum values</param>
			/// <param name="max">The maximum values</param>
			/// <param name="origin">The relative origin to use when clamping. Subtracted from <see cref="v"/> before clamping and added before clamped value is returned.</param>
			public static Vector3Int ClampComponents(
				Vector3Int v, Vector3Int min, Vector3Int max, Vector3Int origin = default
			)
			{
				v -= origin;
				return new Vector3Int
				{
					x = Mathf.Clamp(v.x, min.x, max.x),
					y = Mathf.Clamp(v.y, min.y, max.y),
					z = Mathf.Clamp(v.z, min.z, max.z)
				} + origin;
			}

			public static (Vector2, Vector2) GetMinMax(Vector2 a, Vector2 b)
			{
				return (new Vector2(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y)),
					new Vector2(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y)));
			}

			public static (Vector2Int, Vector2Int) GetMinMax(Vector2Int a, Vector2Int b)
			{
				return (new Vector2Int(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y)),
					new Vector2Int(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y)));
			}

			/// <summary>
			/// Returns the provided value wrapped to the range [<paramref name="min">, <paramref name="max">]
			/// </summary>
			public static float Wrap(float value, float min, float max)
			{
				return value < min ? max - (min - value) % (max - min) : min + (value - min) % (max - min);
			}

			/// <summary>
			/// Returns the provided value wrapped to the range [-360, 360]
			/// </summary>
			public static float WrapAngle(float value) => Wrap(value, -360, 360);

			/// <summary>
			/// Returns the sign of the provided value (1, -1)
			/// </summary>
			/// <remarks>
			/// Will return 1 for value of 0. Use <see cref="SignWithZero(int)"/> if you want 0 to be returned.
			/// </remarks>
			public static int Sign(int value) => value > 0 ? 1 : -1;

			/// <summary>
			/// Returns the sign of the provided value (1, -1) or 0 if the value is 0
			/// </summary>
			public static int SignWithZero(int value) => value == 0 ? 0 : Sign(value);

			/// <summary>
			/// Returns the sign of the provided value (1, -1) or 0 if the value is 0
			/// </summary>
			public static float SignWithZero(float value) => value == 0 ? 0 : Mathf.Sign(value);

			/// <summary>
			/// Returns the provided vector projected on the plane defined by the world up vector
			/// </summary>
			public static Vector3 OnYPlane(Vector3 v) => Vector3.ProjectOnPlane(v, Vector3.up);
		}
	}
}