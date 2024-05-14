using System.Reflection;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

namespace AeLa.Utilities.Tests.EditMode.Tests.EditMode
{
	public class SceneFieldTests
	{
		[Test]
		public void NullField_ImplicitStringIsNull()
		{
			// null SceneField should convert to null string implicitly (no NullReferenceException)
			Assert.IsNull((string)(SceneField)null);
		}

		[Test]
		public void ValidField_ImplicitStringMatchesAssetPath()
		{
			var sceneField = GetValidTestSceneField(out var path);
			Assert.AreEqual(path, (string)sceneField);
		}

		[Test]
		public void ValidField_ScenePathMatchesAssetPath()
		{
			var sceneField = GetValidTestSceneField(out var path);
			Assert.AreEqual(path, sceneField.ScenePath);
		}

		private SceneField GetValidTestSceneField(out string originalScenePath)
		{
			// get the first scene in build settings
			originalScenePath = SceneManager.GetSceneByBuildIndex(0).path;
			var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(originalScenePath);

			// create a scene field
			var sceneField = new SceneField();

			// use reflection to set serialized asset
			var sceneAssetField = typeof(SceneField).GetField("sceneAsset", BindingFlags.Instance | BindingFlags.NonPublic);

			// ReSharper disable once PossibleNullReferenceException
			sceneAssetField.SetValue(sceneField, asset);

			// update internal path
			sceneField.RefreshScenePath();

			return sceneField;
		}
	}
}