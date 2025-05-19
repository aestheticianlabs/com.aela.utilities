using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AeLa.Utilities
{
	[System.Serializable]
	public class WeightedRandomList<T> : IList<WeightedRandomList<T>.WeightedRandomItem>
	{
		[FormerlySerializedAs("items")] public List<WeightedRandomItem> Items;

		[System.Serializable]
		public class WeightedRandomItem
		{
			public T Item;
			public float Weight = 1;
		}

		public WeightedRandomList()
		{
			Items = new();
		}

		public WeightedRandomList(List<WeightedRandomItem> items)
		{
			Items = items;
		}

		public WeightedRandomList(WeightedRandomList<T> source)
		{
			Items = new(source);
		}

		public virtual T GetRandomItem()
		{
			if (Items == null || Items.Count == 0) return default;
			return GetRandomItemInternal(Random.Range(0, GetTotalWeight()));
		}

		public virtual T GetRandomItem(System.Random random)
		{
			if (Items == null || Items.Count == 0) return default;
			return GetRandomItemInternal((float)random.NextDouble() * GetTotalWeight());
		}

		protected virtual T GetRandomItemInternal(float fromWeight)
		{
			foreach (var item in Items)
			{
				if (item == null) continue;
				fromWeight -= item.Weight;
				if (fromWeight <= 0)
				{
					return item.Item;
				}
			}
			
			return Items[0].Item;
		}

		protected virtual float GetTotalWeight()
		{
			float totalWeight = 0;
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i] == null) continue;
				totalWeight += Items[i].Weight;
			}

			return totalWeight;
		}

		#region IList

		public virtual IEnumerator<WeightedRandomItem> GetEnumerator() => Items.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

		public void Add(WeightedRandomItem weightedRandomItem) => Items.Add(weightedRandomItem);

		public void Clear() => Items.Clear();

		public bool Contains(WeightedRandomItem weightedRandomItem) => Items.Contains(weightedRandomItem);

		public void CopyTo(WeightedRandomItem[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);

		public bool Remove(WeightedRandomItem weightedRandomItem) => Items.Remove(weightedRandomItem);

		public int Count => Items.Count;
		public bool IsReadOnly => false;

		public int IndexOf(WeightedRandomItem weightedRandomItem) => Items.IndexOf(weightedRandomItem);

		public void Insert(int index, WeightedRandomItem weightedRandomItem) => Items.Insert(index, weightedRandomItem);

		public void RemoveAt(int index) => Items.RemoveAt(index);

		WeightedRandomItem IList<WeightedRandomItem>.this[int index]
		{
			get => Items[index];
			set => Items[index] = value;
		}

		#endregion
	}
}
