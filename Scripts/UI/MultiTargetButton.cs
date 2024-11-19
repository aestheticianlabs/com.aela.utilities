using System;
using UnityEngine;
using UnityEngine.UI;

namespace AeLa.Utilities.UI
{
	// source: https://discussions.unity.com/t/tint-multiple-targets-with-single-button/556754/11
	[RequireComponent(typeof(TargetGraphicsGroup))]
	public class MultiTargetButton : Button
	{
		private TargetGraphicsGroup targetGraphicsGroup;

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			var targetColor = state switch
			{
				SelectionState.Disabled => colors.disabledColor,
				SelectionState.Highlighted => colors.highlightedColor,
				SelectionState.Normal => colors.normalColor,
				SelectionState.Pressed => colors.pressedColor,
				SelectionState.Selected => colors.selectedColor,
				_ => Color.white
			};

			foreach (var graphic in GetGraphics())
			{
				graphic.CrossFadeColor(targetColor, instant ? 0 : colors.fadeDuration, true, true);
			}
		}

		private Graphic[] GetGraphics()
		{
			if (!targetGraphicsGroup)
			{
				targetGraphicsGroup = GetComponent<TargetGraphicsGroup>();
			}

			return targetGraphicsGroup?.TargetGraphics ?? Array.Empty<Graphic>();
		}
	}
}