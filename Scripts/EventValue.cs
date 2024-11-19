using System;
using JetBrains.Annotations;

namespace AeLa.Utilities
{
	/// <summary>
	/// Holds a value and sends events on change
	/// </summary>
	[PublicAPI]
	public class EventValue<T>
	{
		[Serializable]
		public class UnityEvent : UnityEngine.Events.UnityEvent<T>
		{
		}

		private T value;

		public T Value
		{
			get => value;
			set
			{
				if (value.Equals(this.value)) return;
				this.value = value;
				OnChanged?.Invoke(value);
			}
		}

		public event Action<T> OnChanged;

		public EventValue(Action<T> onChangedCallback)
		{
			if (onChangedCallback != null)
			{
				OnChanged = onChangedCallback;
			}
		}

		public EventValue(T value = default, Action<T> onChangedCallback = null)
		{
			this.value = value;
			if (onChangedCallback != null)
			{
				OnChanged = onChangedCallback;
			}
		}

		public static implicit operator T(EventValue<T> ev) => ev.Value;
	}
}