using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace AeLa.Utilities
{
	public static class CoroutineUtils
	{
		public static IEnumerator DoNextFrame(Action action)
		{
			yield return null;
			action.Invoke();
		}

		/// <summary>
		/// Invokes an action after a delay
		/// </summary>
		public static IEnumerator DoDelayed(Action action, float delay)
		{
			yield return new WaitForSeconds(delay);
			action.Invoke();
		}

		/// <summary>
		/// Executes a coroutine after a delay
		/// </summary>
		public static IEnumerator DoDelayed(IEnumerator routine, float delay)
		{
			yield return new WaitForSeconds(delay);
			yield return routine;
		}

		/// <summary>
		/// Invokes an action after a delay in frames
		/// </summary>
		public static IEnumerator DoDelayedFrames(Action action, int frames)
		{
			yield return new WaitForFrames(frames);
			action.Invoke();
		}

		/// <summary>
		/// Calls the provided callback after the routine is finished
		/// </summary>
		public static IEnumerator DoAfter(Coroutine routine, Action action)
		{
			yield return routine;
			action.Invoke();
		}

		public static IEnumerator DoAfter(IEnumerator routine, Action action)
		{
			yield return routine;
			action.Invoke();
		}

		public static IEnumerator DoAfter(YieldInstruction yieldInstruction, Action action)
		{
			yield return yieldInstruction;
			action.Invoke();
		}

		/// <summary>
		/// Waits until the provided action returns true
		/// </summary>
		public static IEnumerator WaitUntil(Func<bool> predicate)
		{
			yield return new WaitUntil(predicate);
		}

		/// <summary>
		/// Starts a coroutine either with the provided manager or as a globally managed routine
		/// </summary>
		public static Coroutine StartCoroutine(IEnumerator routine, MonoBehaviour manager)
		{
			return manager
				? manager.StartCoroutine(routine)
				: Global.StartCoroutine(routine);
		}

		[SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
		public static class Global
		{
			public static Coroutine StartCoroutine(IEnumerator routine)
			{
				return GlobalCoroutineRunner.Instance.StartCoroutine(routine);
			}

			public static void StopCoroutine(Coroutine coroutine)
			{
				GlobalCoroutineRunner.Instance.StopCoroutine(coroutine);
			}

			/// <summary>
			/// <inheritdoc cref="CoroutineUtils.DoAfter(Coroutine, Action)"/>
			/// </summary>
			public static void DoAfter(Coroutine routine, Action action) =>
				StartCoroutine(CoroutineUtils.DoAfter(routine, action));

			/// <summary>
			/// <inheritdoc cref="CoroutineUtils.DoAfter(IEnumerator, Action)"/>
			/// </summary>
			public static Coroutine DoAfter(IEnumerator routine, Action action) =>
				StartCoroutine(CoroutineUtils.DoAfter(routine, action));

			public static Coroutine DoAfter(YieldInstruction yieldInstruction, Action action) =>
				StartCoroutine(CoroutineUtils.DoAfter(yieldInstruction, action));

			public static Coroutine DoAfter(Func<bool> predicate, Action action) =>
				DoAfter(WaitUntil(predicate), action);

			public static Coroutine DoDelayed(Action action, float delay) =>
				StartCoroutine(CoroutineUtils.DoDelayed(action, delay));

			public static Coroutine DoDelayedFrames(Action action, int delay) =>
				StartCoroutine(CoroutineUtils.DoDelayedFrames(action, delay));

			public static Coroutine DoNextFrame(Action action) => DoDelayed(action, 0);

			public static Coroutine DoNextFixedUpdate(Action action) => DoAfter(new WaitForFixedUpdate(), action);
		}
	}
}