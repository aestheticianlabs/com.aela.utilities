using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AeLa.Utilities.UI
{
	/// <summary>
	/// Fades the attached CanvasGroup in and/or out.
	/// </summary>
	[RequireComponent(typeof(CanvasGroup))]
	public class UIFader : MonoBehaviour
	{
		/// <summary>
		/// The time (in seconds) it takes for this CanvasGroup to fade in.
		/// </summary>
		[Tooltip("The time (in seconds) it takes for this CanvasGroup to fade in.")]
		public float FadeInTime = 0.5f;

		/// <summary>
		/// The curve used to fade in over <see cref="FadeInTime"/>.
		/// </summary>
		[Tooltip("The curve used to fade in over FadeInTime.")]
		public AnimationCurve FadeInCurve = AnimationCurve.EaseInOut(
			timeStart: 0f,
			valueStart: 0f,
			timeEnd: 1f,
			valueEnd: 1f
		);

		/// <summary>
		/// The time (in seconds) it takes for this CanvasGroup to fade out.
		/// </summary>
		[Tooltip("The time (in seconds) it takes for this CanvasGroup to fade out.")]
		public float FadeOutTime = 0.25f;

		/// <summary>
		/// The curve used to fade out over <see cref="FadeOutTime"/>.
		/// </summary>
		[Tooltip("The curve used to fade out over FadeOutTime.")]
		public AnimationCurve FadeOutCurve = AnimationCurve.EaseInOut(
			timeStart: 0f,
			valueStart: 1f,
			timeEnd: 1f,
			valueEnd: 0f
		);

		/// <summary>
		/// The initial alpha of this element.
		/// </summary>
		[Tooltip("The initial alpha of this element.")] [Range(0, 1)]
		public float InitAlpha;

		private CanvasGroup canvasGroup;

		public UnityEvent OnFadeInComplete;
		public UnityEvent OnFadeOutComplete;

		protected void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = InitAlpha;
		}

		/// <summary>
		/// Fade from transparent to opaque.
		/// </summary>
		public Coroutine FadeIn()
		{
			StopAllCoroutines();
			return StartCoroutine(FadeRoutine(1));
		}

		/// <summary>
		/// Fade from opaque to transparent.
		/// </summary>
		public Coroutine FadeOut()
		{
			StopAllCoroutines();
			return StartCoroutine(FadeRoutine(-1));
		}

		private IEnumerator FadeRoutine(int direction)
		{
			var alpha = canvasGroup.alpha;
			var fadeDuration = direction > 0 ? FadeInTime : FadeOutTime;
			var fadeCurve = direction > 0 ? FadeInCurve : FadeOutCurve;
			var progress = (direction > 0 ? alpha : 1f - alpha) * fadeDuration;
			var fadeStartTime = Time.time - progress;

			while (Time.time - fadeStartTime <= fadeDuration)
			{
				progress = (Time.time - fadeStartTime) / fadeDuration;
				canvasGroup.alpha = fadeCurve.Evaluate(progress);
				yield return null;
			}

			// ensure group is fully faded before we're done
			canvasGroup.alpha = fadeCurve.Evaluate(1f);
			yield return new WaitForEndOfFrame();
			(direction > 0 ? OnFadeInComplete : OnFadeOutComplete)?.Invoke();
		}
	}
}