using System;
using System.Collections.Generic;

namespace AeLa.Utilities.Pooling
{
	public readonly struct PooledStack<T> : IDisposable
	{
		public readonly Stack<T> Stack;

		private PooledStack(Stack<T> stack)
		{
			Stack = stack;
		}

		public void Push(T value) => Stack.Push(value);
		public void Pop() => Stack.Pop();
		public bool TryPop(out T value) => Stack.TryPop(out value);
		public T Peek() => Stack.Peek();
		public bool TryPeek(out T value) => Stack.TryPeek(out value);

		public static PooledStack<T> Get()
		{
			StackPool<T>.Get(out var stack);
			return new(stack);
		}

		public void Dispose()
		{
			StackPool<T>.Release(Stack);
		}
	}
}