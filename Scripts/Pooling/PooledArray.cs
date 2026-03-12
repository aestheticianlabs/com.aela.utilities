using System;
using System.Buffers;

namespace AeLa.Utilities.Pooling
{
	public struct PooledArray<T> : IDisposable
	{
		private T[] array;

		public readonly int Length;
		public T[] Array => array;
		public Span<T> Span => Array.AsSpan(0, Length);

		public PooledArray(int length)
		{
			Length = length;
			array = ArrayPool<T>.Shared.Rent(length);
			System.Array.Fill(Array, default, 0, Length);
		}

		public void Dispose()
		{
			if (array == null) return;
			ArrayPool<T>.Shared.Return(array);
			array = null;
		}
	}
}