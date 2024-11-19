using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AeLa.Utilities
{
	/// <summary>
	/// A <see cref="ScriptableObject"/> that holds a list of items and implements <see cref="IList"/>.
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	public abstract class ScriptableList<TItem> : ScriptableObject, IList<TItem>, IReadOnlyList<TItem>
	{
		[SerializeField] private List<TItem> items = new();

		public IEnumerator<TItem> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)items).GetEnumerator();
		}

		public void Add(TItem item)
		{
			items.Add(item);
		}

		public void Clear()
		{
			items.Clear();
		}

		public bool Contains(TItem item)
		{
			return items.Contains(item);
		}

		public void CopyTo(TItem[] array, int arrayIndex)
		{
			items.CopyTo(array, arrayIndex);
		}

		public bool Remove(TItem item)
		{
			return items.Remove(item);
		}

		public int Count => items.Count;

		public bool IsReadOnly => false;

		public int IndexOf(TItem item)
		{
			return items.IndexOf(item);
		}

		public void Insert(int index, TItem item)
		{
			items.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			items.RemoveAt(index);
		}

		public TItem this[int index] { get => items[index]; set => items[index] = value; }
	}
}