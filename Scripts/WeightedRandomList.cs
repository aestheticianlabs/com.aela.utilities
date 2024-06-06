using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AeLa.Utilities
{
	[System.Serializable]
	public class WeightedRandomList<T> : IList<WeightedRandomList<T>.WeightedRandomItem>
	{
		[SerializeField] private List<WeightedRandomItem> items;

		[System.Serializable]
		public class WeightedRandomItem
		{
			public T Item;
			public float Weight = 1;
		}

		public WeightedRandomList(WeightedRandomList<T> source)
		{
			items = new(source);
		}

		public T GetRandomItem()
		{
			if (items == null || items.Count == 0) return default;
			return GetRandomItemInternal(Random.Range(0, GetTotalWeight()));
		}

		public T GetRandomItem(System.Random random)
		{
			if (items == null || items.Count == 0) return default;
			return GetRandomItemInternal((float)random.NextDouble() * GetTotalWeight());
		}

		private T GetRandomItemInternal(float fromWeight)
		{
			foreach (var item in items)
			{
				if (item == null) continue;
				fromWeight -= item.Weight;
				if (fromWeight <= 0)
				{
					return item.Item;
				}
			}
			
			return items[0].Item;
		}

		private float GetTotalWeight()
		{
			float totalWeight = 0;
			for (int i = 0; i < items.Count; i++)
			{
				if (items[i] == null) continue;
				totalWeight += items[i].Weight;
			}

			return totalWeight;
		}

		#region IList

		public IEnumerator<WeightedRandomItem> GetEnumerator() => items.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

		public void Add(WeightedRandomItem weightedRandomItem) => items.Add(weightedRandomItem);

		public void Clear() => items.Clear();

		public bool Contains(WeightedRandomItem weightedRandomItem) => items.Contains(weightedRandomItem);

		public void CopyTo(WeightedRandomItem[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

		public bool Remove(WeightedRandomItem weightedRandomItem) => items.Remove(weightedRandomItem);

		public int Count => items.Count;
		public bool IsReadOnly => false;

		public int IndexOf(WeightedRandomItem weightedRandomItem) => items.IndexOf(weightedRandomItem);

		public void Insert(int index, WeightedRandomItem weightedRandomItem) => items.Insert(index, weightedRandomItem);

		public void RemoveAt(int index) => items.RemoveAt(index);

		WeightedRandomItem IList<WeightedRandomItem>.this[int index]
		{
			get => items[index];
			set => items[index] = value;
		}

		#endregion
	}
}