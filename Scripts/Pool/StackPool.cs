using System.Collections.Generic;
using UnityEngine.Pool;

namespace AeLa.Utilities.Pool
{
	public static class StackPool<T>
	{
		internal static readonly ObjectPool<Stack<T>> pool = new(() => new(), actionOnRelease: s => s.Clear());

		public static Stack<T> Get() => pool.Get();
		public static PooledObject<Stack<T>> Get(out Stack<T> element) => pool.Get(out element);
		public static void Release(Stack<T> element) => pool.Release(element);
	}
}