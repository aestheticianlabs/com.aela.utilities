using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace AeLa.Utilities.Pooling
{
	public struct PooledList<T> : IList<T>, IDisposable
	{
		public List<T> List;

		private PooledList(List<T> list)
		{
			List = list;
		}

		// structs can't have parameterless constructors so this is a replacement
		public static PooledList<T> Get()
		{
			ListPool<T>.Get(out var list);
			return new(list);
		}

		public void Dispose()
		{
			if (List == null) return;
			ListPool<T>.Release(List);
		}

		private void ThrowIfDisposed()
		{
			if (List == null)
			{
				throw new ObjectDisposedException(nameof(PooledList<T>));
			}
		}

		#region IList

		public IEnumerator<T> GetEnumerator()
		{
			ThrowIfDisposed();
			return List.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			ThrowIfDisposed();
			return ((IEnumerable)List).GetEnumerator();
		}

		public void Add(T item)
		{
			ThrowIfDisposed();
			List.Add(item);
		}

		public void Clear()
		{
			ThrowIfDisposed();
			List.Clear();
		}

		public bool Contains(T item)
		{
			ThrowIfDisposed();
			return List.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			ThrowIfDisposed();
			List.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			ThrowIfDisposed();
			return List.Remove(item);
		}

		public int Count
		{
			get
			{
				ThrowIfDisposed();
				return List.Count;
			}
		}

		public bool IsReadOnly => false;

		public int IndexOf(T item)
		{
			ThrowIfDisposed();
			return List.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			ThrowIfDisposed();
			List.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			ThrowIfDisposed();
			List.RemoveAt(index);
		}

		public T this[int index]
		{
			get
			{
				ThrowIfDisposed();
				return List[index];
			}
			set
			{
				ThrowIfDisposed();
				List[index] = value;
			}
		}

		#endregion
	}
}