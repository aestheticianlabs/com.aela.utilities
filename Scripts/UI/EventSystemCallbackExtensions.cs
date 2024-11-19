using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AeLa.Utilities.UI
{
	[PublicAPI]
	public static class EventSystemCallbackExtensions
	{
		public static void OnPointerEnter(this GameObject c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerEnterCallback, PointerEventData>(c, callback);
		}

		public static void OnPointerExit(this GameObject c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerExitCallback, PointerEventData>(c, callback);
		}

		public static void OnPointerMove(this GameObject c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerMoveCallback, PointerEventData>(c, callback);
		}

		public static void OnPointerClick(this GameObject c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerClickCallback, PointerEventData>(c, callback);
		}

		public static void OnSelect(this GameObject c, Action<BaseEventData> callback)
		{
			AddEventListener<OnSelectCallback, BaseEventData>(c, callback);
		}

		public static void OnDeselect(this GameObject c, Action<BaseEventData> callback)
		{
			AddEventListener<OnDeselectCallback, BaseEventData>(c, callback);
		}

		public static void RemovePointerEnterListener(this GameObject c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerEnterCallback, PointerEventData>(c, callback);
		}

		public static void RemovePointerExitListener(this GameObject c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerExitCallback, PointerEventData>(c, callback);
		}

		public static void RemovePointerMoveListener(this GameObject c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerMoveCallback, PointerEventData>(c, callback);
		}

		public static void RemovePointerClickListener(this GameObject c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerClickCallback, PointerEventData>(c, callback);
		}

		public static void RemoveSelectListener(this GameObject c, Action<BaseEventData> callback)
		{
			RemoveEventListener<OnSelectCallback, BaseEventData>(c, callback);
		}

		public static void RemoveDeselectListener(this GameObject c, Action<BaseEventData> callback)
		{
			RemoveEventListener<OnDeselectCallback, BaseEventData>(c, callback);
		}

		public static void OnPointerEnter(this Component c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerEnterCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void OnPointerExit(this Component c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerExitCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void OnPointerMove(this Component c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerMoveCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void OnPointerClick(this Component c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerClickCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void OnPointerDown(this GameObject go, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerDownCallback, PointerEventData>(go, callback);
		}

		public static void OnPointerDown(this Component c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerDownCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void OnPointerUp(this GameObject go, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerUpCallback, PointerEventData>(go, callback);
		}

		public static void OnPointerUp(this Component c, Action<PointerEventData> callback)
		{
			AddEventListener<OnPointerUpCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void OnSelect(this Component c, Action<BaseEventData> callback)
		{
			AddEventListener<OnSelectCallback, BaseEventData>(c.gameObject, callback);
		}

		public static void OnDeselect(this Component c, Action<BaseEventData> callback)
		{
			AddEventListener<OnDeselectCallback, BaseEventData>(c.gameObject, callback);
		}

		public static void RemovePointerEnterListener(this Component c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerEnterCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void RemovePointerExitListener(this Component c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerExitCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void RemovePointerMoveListener(this Component c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerMoveCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void RemovePointerClickListener(this Component c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerClickCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void RemovePointerDownListener(this GameObject go, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerDownCallback, PointerEventData>(go, callback);
		}

		public static void RemovePointerDownListener(this Component c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerDownCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void RemovePointerUpListener(this GameObject go, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerUpCallback, PointerEventData>(go, callback);
		}

		public static void RemovePointerUpListener(this Component c, Action<PointerEventData> callback)
		{
			RemoveEventListener<OnPointerUpCallback, PointerEventData>(c.gameObject, callback);
		}

		public static void RemoveSelectListener(this Component c, Action<BaseEventData> callback)
		{
			RemoveEventListener<OnSelectCallback, BaseEventData>(c.gameObject, callback);
		}

		public static void RemoveDeselectListener(this Component c, Action<BaseEventData> callback)
		{
			RemoveEventListener<OnDeselectCallback, BaseEventData>(c.gameObject, callback);
		}

		private static void AddEventListener<TComponent, TEventData>(GameObject target, Action<TEventData> callback)
			where TComponent : EventSystemCallback<TEventData>
		{
			if (!target.TryGetComponentCached<TComponent>(out var handler))
			{
				handler = target.AddComponent<TComponent>();
			}

			handler.Callback += callback;
		}

		private static void RemoveEventListener<TComponent, TEventData>(GameObject target, Action<TEventData> callback)
			where TComponent : EventSystemCallback<TEventData>
		{
			if (!target.TryGetComponentCached(out TComponent handler))
			{
				return;
			}

			handler.Callback -= callback;
		}
	}

	public class EventSystemCallback<TEventData> : MonoBehaviour
	{
		public event Action<TEventData> Callback;

		protected void Invoke(TEventData eventData) => Callback?.Invoke(eventData);
	}

	public class OnPointerEnterCallback : EventSystemCallback<PointerEventData>, IPointerEnterHandler
	{
		public void OnPointerEnter(PointerEventData eventData)
		{
			Invoke(eventData);
		}
	}

	public class OnPointerExitCallback : EventSystemCallback<PointerEventData>, IPointerExitHandler
	{
		public void OnPointerExit(PointerEventData eventData)
		{
			Invoke(eventData);
		}
	}

	public class OnPointerMoveCallback : EventSystemCallback<PointerEventData>, IPointerMoveHandler
	{
		public void OnPointerMove(PointerEventData eventData)
		{
			Invoke(eventData);
		}
	}

	public class OnPointerClickCallback : EventSystemCallback<PointerEventData>, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			Invoke(eventData);
		}
	}

	public class OnPointerDownCallback : EventSystemCallback<PointerEventData>, IPointerDownHandler
	{
		public void OnPointerDown(PointerEventData eventData)
		{
			Invoke(eventData);
		}
	}

	public class OnPointerUpCallback : EventSystemCallback<PointerEventData>, IPointerUpHandler
	{
		public void OnPointerUp(PointerEventData eventData)
		{
			Invoke(eventData);
		}
	}

	public class OnSelectCallback : EventSystemCallback<BaseEventData>, ISelectHandler
	{
		public void OnSelect(BaseEventData eventData)
		{
			Invoke(eventData);
		}
	}

	public class OnDeselectCallback : EventSystemCallback<BaseEventData>, IDeselectHandler
	{
		public void OnDeselect(BaseEventData eventData)
		{
			Invoke(eventData);
		}
	}
}