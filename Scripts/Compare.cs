using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace AeLa.Utilities
{
	[PublicAPI]
	public static class Compare
	{
		/// <summary>
		/// Can be used with a comparer in a Sort call to sort in descending order
		/// </summary>
		/// <example><code>positions.Sort((a, b) => Descend(Distance(a, b, origin)))</code></example>
		public static int Descend(int comparison) => -comparison;

		/// <summary>
		/// Compares the distance between a and b vectors and origin
		/// </summary>
		/// <returns>Less than 0 if a is closer than b, greater than 0 if b is closer than a, 0 if they are of equal distance.</returns>
		public static int Distance(Vector3 a, Vector3 b, Vector3 origin)
		{
			return Mathf.RoundToInt(Mathf.Sign((a - origin).sqrMagnitude - (b - origin).sqrMagnitude));
		}

		/// <inheritdoc cref="Distance(UnityEngine.Vector3,UnityEngine.Vector3,UnityEngine.Vector3)"/>
		public static int Distance(Transform a, Transform b, Vector3 origin) =>
			Distance(a.position, b.position, origin);

		/// <inheritdoc cref="Distance(UnityEngine.Vector3,UnityEngine.Vector3,UnityEngine.Vector3)"/>
		public static int Distance(GameObject a, GameObject b, Vector3 origin) =>
			Distance(a.transform, b.transform, origin);

		/// <inheritdoc cref="Distance(UnityEngine.Vector3,UnityEngine.Vector3,UnityEngine.Vector3)"/>
		public static int Distance(Component a, Component b, Vector3 origin) =>
			Distance(a.gameObject, b.gameObject, origin);

		public static T Min<T>(T a, T b) where T : IComparable
		{
			return Comparer<T>.Default.Compare(a, b) <= 0 ? a : b;
		}

		public static T Max<T>(T a, T b) where T : IComparable
		{
			return Comparer<T>.Default.Compare(a, b) <= 0 ? b : a;
		}
	}
}