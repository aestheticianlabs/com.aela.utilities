using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace AeLa.Utilities.Pooling
{
	public struct PooledDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDisposable
	{
		public Dictionary<TKey, TValue> Dictionary { get; private set; }

		private PooledDictionary(Dictionary<TKey, TValue> dictionary)
		{
			Dictionary = dictionary;
		}

		public void Dispose()
		{
			if (Dictionary != null)
			{
				DictionaryPool<TKey, TValue>.Release(Dictionary);
				Dictionary = null;
			}
		}

		public static PooledDictionary<TKey, TValue> Get()
		{
			return new(DictionaryPool<TKey, TValue>.Get());
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return Dictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)Dictionary).GetEnumerator();
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			((IDictionary<TKey, TValue>)Dictionary).Add(item);
		}

		public void Clear()
		{
			Dictionary.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)Dictionary).Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((IDictionary<TKey, TValue>)Dictionary).CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)Dictionary).Remove(item);
		}

		public int Count => Dictionary.Count;

		public bool IsReadOnly => ((IDictionary<TKey, TValue>)Dictionary).IsReadOnly;

		public void Add(TKey key, TValue value)
		{
			Dictionary.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return Dictionary.ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			return Dictionary.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return Dictionary.TryGetValue(key, out value);
		}

		public readonly TValue this[TKey key]
		{
			get => Dictionary[key];
			set => Dictionary[key] = value;
		}

		public ICollection<TKey> Keys => Dictionary.Keys;

		public ICollection<TValue> Values => Dictionary.Values;
	}
}