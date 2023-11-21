using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace AeLa.Utilities.Tests.PlayMode
{
	public class LifecycleEventExtensionTests
	{
		[UnityTest]
		public IEnumerator OnEnable_ReceivesCallback()
		{
			var go = new GameObject();

			var calls = 0;

			go.OnEnable(Listener);

			go.SetActive(false);
			yield return null;
			go.SetActive(true);

			Assert.AreEqual(calls, 1, "Callback not called on OnEnable");

			go.RemoveOnEnableCallback(Listener);

			go.SetActive(false);
			yield return null;
			go.SetActive(true);

			Assert.AreEqual(calls, 1, "Callback called after being removed");

			Object.Destroy(go);

			yield break;
			void Listener(GameObject _) => calls++;
		}

		[UnityTest]
		public IEnumerator OnDisable_ReceivesCallback()
		{
			var go = new GameObject();

			var calls = 0;

			go.OnDisable(Listener);

			go.SetActive(false);
			yield return null;

			Assert.AreEqual(calls, 1, "Callback not called on OnDisable");

			go.RemoveOnDisableCallback(Listener);

			go.SetActive(true);
			yield return null;
			go.SetActive(false);

			Assert.AreEqual(calls, 1, "Callback called after being removed");

			Object.Destroy(go);

			yield break;
			void Listener(GameObject _) => calls++;
		}

		[UnityTest]
		public IEnumerator OnDestroy_ReceivesCallback()
		{
			var go = new GameObject();

			var called = false;
			var called2 = false;

			go.OnDestroy(Listener);
			go.OnDestroy(BadListener);
			yield return null;

			go.RemoveOnDestroyCallback(BadListener);

			Object.Destroy(go);
			yield return null;

			Assert.That(called, "Callback not called on destroy");
			Assert.IsFalse(called2, "Removed callback was called");

			yield break;
			void Listener(GameObject _) => called = true;
			void BadListener(GameObject _) => called2 = true;
		}
	}
}