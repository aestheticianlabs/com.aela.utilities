using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace AeLa.Utilities.Pooling
{
	public readonly struct PooledHashSet<T> : IReadOnlyCollection<T>, ISet<T>,
		IDisposable
	{
		public readonly HashSet<T> HashSet;

		private PooledHashSet(HashSet<T> hashSet)
		{
			HashSet = hashSet;
		}

		public static PooledHashSet<T> Get()
		{
			return new(HashSetPool<T>.Get());
		}

		public void Dispose()
		{
			HashSetPool<T>.Release(HashSet);
		}

		public static implicit operator HashSet<T>(PooledHashSet<T> pooledHashSet) => pooledHashSet.HashSet;

		#region Implmentation

		public IEnumerator<T> GetEnumerator()
		{
			return HashSet.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)HashSet).GetEnumerator();
		}

		void ICollection<T>.Add(T item) => Add(item);
		public bool Add(T item) => HashSet.Add(item);

		public void ExceptWith(IEnumerable<T> other)
		{
			HashSet.ExceptWith(other);
		}

		public void IntersectWith(IEnumerable<T> other)
		{
			HashSet.IntersectWith(other);
		}

		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return HashSet.IsProperSubsetOf(other);
		}

		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return HashSet.IsProperSupersetOf(other);
		}

		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return HashSet.IsSubsetOf(other);
		}

		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return HashSet.IsSupersetOf(other);
		}

		public bool Overlaps(IEnumerable<T> other)
		{
			return HashSet.Overlaps(other);
		}

		public bool SetEquals(IEnumerable<T> other)
		{
			return HashSet.SetEquals(other);
		}

		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			HashSet.SymmetricExceptWith(other);
		}

		public void UnionWith(IEnumerable<T> other)
		{
			HashSet.UnionWith(other);
		}

		bool ISet<T>.Add(T item)
		{
			return HashSet.Add(item);
		}

		public void Clear()
		{
			HashSet.Clear();
		}

		public bool Contains(T item)
		{
			return HashSet.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			HashSet.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return HashSet.Remove(item);
		}

		public int Count => HashSet.Count;

		public bool IsReadOnly => false;

		#endregion
	}
}