using UnityEngine;

namespace AeLa.Utilities.Physics
{
	/// <summary>
	/// Utility functions for raycasting
	/// </summary>
	public static class Raycast
	{
		private static RaycastHit[] resultsArr = new RaycastHit[100];

		/// <summary>
		/// Sends a raycast without GC allocation using an internally managed results array.
		/// </summary>
		/// <param name="origin">The origin for the ray</param>
		/// <param name="direction">The direction of the ray</param>
		/// <param name="results">A shared array of length 100</param>
		/// <param name="maxDistance">The maximum distance to cast</param>
		/// <param name="layerMask">The LayerMask to use for the cast</param>
		/// <param name="queryTriggerInteraction">Whether trigger zones should interact with this raycast</param>
		/// <returns>The number of results in the <see cref="results"/> array</returns>
		public static int NonAlloc(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit[] results,
			float maxDistance,
			int layerMask,
			QueryTriggerInteraction queryTriggerInteraction
		)
		{
			results = resultsArr;

			var count = UnityEngine.Physics.RaycastNonAlloc(
				origin, direction, results, maxDistance, layerMask, queryTriggerInteraction
			);

			if (count > results.Length)
				Debug.LogWarning($"Raycast results count higher than array length ({resultsArr.Length}");

			return count;
		}

		/// <inheritdoc cref="NonAlloc(UnityEngine.Vector3,UnityEngine.Vector3,out UnityEngine.RaycastHit[],float,int,UnityEngine.QueryTriggerInteraction)"/>
		public static int NonAlloc(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit[] results,
			float maxDistance,
			int layerMask
		) => NonAlloc(origin, direction, out results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

		/// <inheritdoc cref="NonAlloc(UnityEngine.Vector3,UnityEngine.Vector3,out UnityEngine.RaycastHit[],float,int,UnityEngine.QueryTriggerInteraction)"/>
		public static int NonAlloc(
			Ray ray,
			out RaycastHit[] results,
			float maxDistance,
			int layerMask,
			QueryTriggerInteraction queryTriggerInteraction
		) => NonAlloc(ray.origin, ray.direction, out results, maxDistance, layerMask, queryTriggerInteraction);

		/// <inheritdoc cref="NonAlloc(UnityEngine.Vector3,UnityEngine.Vector3,out UnityEngine.RaycastHit[],float,int,UnityEngine.QueryTriggerInteraction)"/>
		public static int NonAlloc(Ray ray, out RaycastHit[] results, float maxDistance, int layerMask)
			=> NonAlloc(ray.origin, ray.direction, out results, maxDistance, layerMask);
	}
}