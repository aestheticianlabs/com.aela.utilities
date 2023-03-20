using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace AeLa.Utilities
{
	public class Timer : MonoBehaviour
	{
		public float Duration = 3;
		public bool StartOnEnable;

		public UnityEvent OnStarted;
		public UnityEvent OnFinished;

		private bool started;
		private float startTime;
		private float duration;

		private void OnEnable()
		{
			if (StartOnEnable)
			{
				StartTimer();
			}
		}

		private void Update()
		{
			if (!started) return;

			if (Time.time - startTime >= duration)
			{
				// timer complete
				StopTimer();
				OnFinished?.Invoke();
			}
		}

		public void StartTimer(bool force, float overrideDuration)
		{
			if (!force && started) return;

			enabled = true;
			started = true;
			duration = overrideDuration;
			startTime = Time.time;
			OnStarted?.Invoke();
		}

		public void StartTimer(bool force) => StartTimer(force, Duration);
		public void StartTimer() => StartTimer(false);

		// for UnityEvents in editor
		[PublicAPI]
		public void ForceStartTimer(float overrideDuration) => StartTimer(true, overrideDuration);

		public void StopTimer()
		{
			started = false;
		}
	}
}