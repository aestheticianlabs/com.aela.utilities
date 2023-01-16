using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AeLa.Utilities.UI
{
	/// <summary>
	/// Sends messages/events on attached canvas enable/disable
	/// </summary>
	public class CanvasEnableEventMessages : MonoBehaviour
	{
		/// <summary>
		/// Objects to be enabled/disabled with canvas
		/// </summary>
		[Tooltip("Objects to be enabled/disabled with canvas")]
		[SerializeField] private List<Object> controlObjects;
		
		private Canvas canvas;
		private bool canvasEnabled;

		public BoolUnityEvent OnEnabledChanged;
		
		private void Awake()
		{
			canvas = GetComponent<Canvas>();
			CanvasEnabledChanged();
		}

		private void Update()
		{
			if (canvasEnabled != canvas.enabled)
			{
				CanvasEnabledChanged();
			}
		}

		private void CanvasEnabledChanged()
		{
			canvasEnabled = canvas.enabled;
			UpdateControlObjects();
			OnEnabledChanged?.Invoke(canvas.enabled);
		}

		private void UpdateControlObjects()
		{
			foreach (var o in controlObjects)
			{
				switch (o)
				{
					case Behaviour b: b.enabled = canvas.enabled;
						break;
					case GameObject go: go.SetActive(canvas.enabled);
						break;
					default: Debug.LogError($"[CanvasEnableEventMessages] Unhandled object {o}. Control objects only support GameObject or Behaviour types.", this);
						break;
				}
			}
		}
	}
}