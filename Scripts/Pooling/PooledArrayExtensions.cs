using System.Collections.Generic;

namespace AeLa.Utilities.Pooling
{
	public static class PooledArrayExtensions
	{
		public static PooledArray<T> ToPooledArray<T>(this IList<T> list)
		{
			var arr = new PooledArray<T>(list.Count);
			list.CopyTo(arr.Array, 0);
			return arr;
		}

		// explicit implementation prevents boxing from interface
		public static PooledArray<T> ToPooledArray<T>(this PooledList<T> list)
		{
			var arr = new PooledArray<T>(list.Count);
			list.CopyTo(arr.Array, 0);
			return arr;
		}
	}
}