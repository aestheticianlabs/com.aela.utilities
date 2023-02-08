using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AeLa.Utilities
{
	public static class Extensions
	{
		/// <summary>
		/// <see cref="string.Contains"/> that allows StringComparison argument
		/// </summary>
		public static bool Contains(this string source, string value, StringComparison comparison)
		{
			return source?.IndexOf(value, comparison) >= 0;
		}

		public static float DistanceSq(this Vector3 from, Vector3 to) => (to - from).sqrMagnitude;
		public static float DistanceSq(this Transform from, Transform to) => DistanceSq(from.position, to.position);
		public static Vector3 To(this Vector3 from, Vector3 to) => to - from;
		public static float Sq(this float v) => v * v;

		/// <summary>
		/// Rounds a value to the nearest integer, up if positive (i.e. 0.2 => 1) and down if negative (i.e. -0.2 => -1)
		/// </summary>
		public static int CeilToIntSigned(this float v)
		{
			return Mathf.CeilToInt(Mathf.Abs(v)) * (int)Mathf.Sign(v);
		}

		/// <summary>
		/// Rounds components to the nearest integer, up if positive (i.e. 0.2 => 1) and down if negative (i.e. -0.2 => -1)
		/// </summary>
		public static Vector3Int CeilToIntSigned(this Vector3 v)
		{
			return new Vector3Int
			{
				x = v.x.CeilToIntSigned(),
				y = v.y.CeilToIntSigned(),
				z = v.z.CeilToIntSigned()
			};
		}

		public static Color WithAlpha(this Color color, float a)
		{
			var c = color;
			c.a = a;
			return c;
		}

		public static bool Contains(this LayerMask mask, int layer)
		{
			return ((1 << layer) & mask) != 0;
		}

		/// <summary>
		/// Copies world position, rotation, and scale to this transform.
		/// </summary>
		public static void CopyValues(this Transform to, Transform from, bool includeScale = false)
		{
			to.position = from.position;
			to.rotation = from.rotation;
			if (includeScale) to.localScale = to.InverseTransformVector(from.lossyScale);
		}

		public static string ToListString(this IEnumerable enumerable)
		{
			var sb = new StringBuilder();

			foreach (var o in enumerable)
			{
				if (sb.Length > 0) sb.Append(", ");
				sb.Append(o);
			}

			return sb.ToString();
		}

		public static T RandomElement<T>(this IList<T> list)
		{
			var i = Random.Range(0, list.Count);
			return list[i];
		}

		public static bool TryFind<T>(this IEnumerable<T> collection, Predicate<T> predicate, out T match)
		{
			foreach (var item in collection)
			{
				if (predicate(item))
				{
					match = item;
					return true;
				}
			}

			match = default;
			return false;
		}

		public static bool TryFindType<TCollection, TMatch>(
			this IEnumerable<TCollection> collection, out TMatch match
		)
		{
			foreach (var item in collection)
			{
				if (item is TMatch m)
				{
					match = m;
					return true;
				}
			}

			match = default;
			return false;
		}

		/// <summary>
		/// Removes each item in the provided list from this list.
		/// <br/>
		/// Does the opposite of <see cref="List{T}.AddRange"/>.
		/// </summary>
		public static void RemoveRange<T>(this List<T> list, List<T> items)
		{
			foreach (var item in items)
			{
				list.Remove(item);
			}
		}

		/// <summary>
		/// Returns the last item in the list.
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Last<T>(this IList<T> list)
		{
			if (list.Count == 0)
				throw new InvalidOperationException("The list is empty.");

			return list[list.Count - 1];
		}

		// handy helper function since Deconstruct isn't implemented in the version of .NET we're using
		// use case: foreach (var (key, value) in dictionary) { ... }
		public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> kvp, out T1 key, out T2 value)
		{
			key = kvp.Key;
			value = kvp.Value;
		}

		public static bool TryGetComponentInChildren<T>(this GameObject go, out T component)
		{
			return !Util.IsNullOrUnityNull(component = go.GetComponentInChildren<T>());
		}

		public static bool TryGetParentComponent<T>(this GameObject go, out T component)
		{
			return !Util.IsNullOrUnityNull(component = go.GetComponentInParent<T>());
		}

		/// <summary>
		/// Returns whether the <see cref="TimeSpan"/> is within the provided range.
		/// </summary>
		public static bool IsBetween(this TimeSpan timeSpan, TimeSpan min, TimeSpan max)
		{
			return min <= max ? timeSpan >= min && timeSpan <= max : timeSpan >= min || timeSpan <= max;
		}

		public static T GetRequiredComponent<T>(this Component component) =>
			component.gameObject.GetRequiredComponent<T>();

		public static T GetRequiredComponent<T>(this GameObject gameObject, Component context = null)
		{
			if (!gameObject.TryGetComponent(out T ret))
			{
				var message = $"{gameObject} is missing required component {typeof(T)}";
				if (context) message += $" ({context})";
				Debug.LogError(message, context ? (Object)context : gameObject);
			}

			return ret;
		}

		public static bool TryGetValue<TKey, TDict, T>(this TDict dict, TKey key, out T val)
			where TDict : IDictionary<TKey, object>
		{
			val = default;
			if (!dict.TryGetValue(key, out var value)) return false;

			if (value is T castVal)
			{
				val = castVal;
				return true;
			}

			Debug.LogError($"Dictionary contains key {key} but value is not type {typeof(T)}");
			val = default;
			return false;
		}

		/// <summary>
		/// Shuffles a list in place while enumerating.
		/// Handy if you want a random order of items from a list
		/// AND don't care that the original list is mutated in the process.
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <remarks>Modifies the list being iterated!</remarks>
		public static IEnumerable<T> ShuffleEnumerate<T>(this IList<T> list)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				var swap = Random.Range(0, i + 1);
				yield return list[swap];
				list[swap] = list[i];
			}
		}

		/// <summary>
		/// Adds the component if none already added.
		/// </summary>
		/// <param name="go"></param>
		/// <param name="component">The existing or added component</param>
		/// <typeparam name="T">The component to add</typeparam>
		/// <returns>Whether or not the component was added (false if already on GameObject)</returns>
		public static bool GetOrAddComponent<T>(this GameObject go, out T component) where T : Component
		{
			if (go.TryGetComponent(out component)) return true;

			component = go.AddComponent<T>();
			return false;
		}

		public static T NearestTo<T>(this IEnumerable<T> collection, Vector2 position) where T : Component
		{
			T nearest = null;
			var nearestDist = float.PositiveInfinity;

			foreach (var item in collection)
			{
				var dist = ((Vector2)item.transform.position - position).sqrMagnitude;
				if (dist < nearestDist)
				{
					nearest = item;
					nearestDist = dist;
				}
			}

			return nearest;
		}

		public static T NearestTo<T>(this IEnumerable<T> collection, Vector3 position) where T : Component
		{
			T nearest = null;
			var nearestDist = float.PositiveInfinity;

			foreach (var item in collection)
			{
				var dist = (item.transform.position - position).sqrMagnitude;
				if (dist < nearestDist)
				{
					nearest = item;
					nearestDist = dist;
				}
			}

			return nearest;
		}
		
		/// <summary>
		/// Gets the connected anchor for this joint in world space, whether or not there is a connected body.
		/// </summary>
		public static Vector3 GetWorldAnchor(this Joint joint) =>
			joint.connectedBody
				? joint.connectedAnchor + joint.connectedBody.transform.position
				: joint.connectedAnchor;

		/// <summary>
		/// Sets the connected anchor for this joint in world space.
		/// </summary>
		public static void SetWorldAnchor(this Joint joint, Vector3 worldAnchor, Rigidbody connectedBody = null)
		{
			if (connectedBody)
			{
				joint.connectedBody = connectedBody;
				joint.connectedAnchor = worldAnchor - connectedBody.transform.position;
				return;
			}

			joint.connectedAnchor = worldAnchor;
		}
	}
}