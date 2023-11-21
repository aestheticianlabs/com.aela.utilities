using System.Collections;
using AeLa.Utilities.EditorTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AeLa.Utilities.Tests.PlayMode
{
	public class ComponentCacheTests
	{
		[UnityTest]
		public IEnumerator GetCached_ReturnsComponent()
		{
			var go = new GameObject();

			Assert.That(go.TryGetComponentCached(out Transform t) && t, "Failed to get Transform on GameObject");

			yield return null;

			Assert.That(go.TryGetComponentCached(out t) && t, "Failed to get Transform on GameObject");

			Object.Destroy(go);
		}

		[UnityTest]
		public IEnumerator GetCached_DoesNotReturnAfterDestroy()
		{
			var go = new GameObject();

			Assert.That(go.TryGetComponentCached(out Transform t) && t, "Failed to get Transform on GameObject");
			yield return null;

			Object.Destroy(go);
			yield return null;

			Assert.IsFalse(go.TryGetComponentCached(out t), "Got component from cache after object was destroyed");

		}

		[UnityTest]
		public IEnumerator GetCached_DoesNotReturnAfterRemoved()
		{
			var go = new GameObject();
			go.AddComponent<EditorComment>();

			Assert.That(go.TryGetComponentCached(out EditorComment c) && c, "Failed to get component on GameObject");
			yield return null;

			Object.Destroy(c);
			yield return null;

			Assert.IsFalse(go.TryGetComponentCached(out c), "Got component from cache after component was removed");

			Object.Destroy(go);
		}

		[Test]
		public void GetOrAddComponentCached_ReturnsCachedAfterAdd()
		{
			var go = new GameObject();
			var component = go.GetOrAddComponentCached<EditorComment>();

			Assert.That(component, "Component was not returned from GetOrAddComponent");

			var c2 = go.GetOrAddComponentCached<EditorComment>();

			Assert.AreEqual(component, c2);

			Object.Destroy(go);
		}

		[UnityTest]
		public IEnumerator GetOrAddComponentCached_AddsNewAfterRemoved()
		{
			var go = new GameObject();
			var component = go.GetOrAddComponentCached<EditorComment>();

			Assert.That(component, "Component was not returned from GetOrAddComponent");

			var c2 = go.GetOrAddComponentCached<EditorComment>();

			Assert.AreEqual(component, c2);

			Object.Destroy(component);
			yield return null;

			c2 = go.GetOrAddComponentCached<EditorComment>();
			Assert.AreNotEqual(component, c2);

			Object.Destroy(go);
		}
	}
}