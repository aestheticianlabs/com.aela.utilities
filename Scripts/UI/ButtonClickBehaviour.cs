using UnityEngine;
using UnityEngine.UI;

namespace AeLa.Utilities.UI
{
	[RequireComponent(typeof(Button))]
	public abstract class ButtonClickBehaviour : MonoBehaviour
	{
		protected Button Button;

		protected virtual void Awake()
		{
			Button = GetComponent<Button>();
			Button.onClick.AddListener(Button_OnClick);
		}

		protected virtual void OnDestroy()
		{
			Button.onClick.RemoveListener(Button_OnClick);
		}

		protected abstract void Button_OnClick();
	}
}