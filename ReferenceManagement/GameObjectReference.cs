using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AeLa.Utilities.ReferenceManagement
{
	[CreateAssetMenu(fileName="New Game Object Reference", menuName = "Game Object Reference")]
	public class GameObjectReference : ScriptableObject
	{
		/// <summary>
		/// Should there only ever be one active reference of this type (singleton)?
		/// </summary>
		[Tooltip("Should there only ever be one active reference of this type (singleton?")]
		[SerializeField] private bool isUnique;
		public bool IsUnique => isUnique;

		/// <summary>
		/// If 'Is Unique' checked and there is already an existing reference, should the new reference replace it?
		/// </summary>
		[Tooltip("If 'Is Unique' checked and there is already an existing reference, should the new reference replace it?")]
		[EnableIf("IsUnique")]
		[SerializeField] private bool replaceExisting;
		public bool ReplaceExisting => replaceExisting;

		/// <summary>
		/// Unique key that can be used to find this reference.
		/// </summary>
		[Tooltip("Unique key that can be used to find this reference.")]
		[SerializeField] private string key;
		public string Key => key;

		private void OnValidate()
		{
			if (!IsUnique) replaceExisting = false;
		}

		private void OnDestroy()
		{
			ReferencableGameObject.OnReferenceDestroy(this);
		}

		public GameObject FindSingle() => ReferencableGameObject.FindSingle(this);
		public bool TryFindSingle(out GameObject go) =>
			ReferencableGameObject.TryFindSingle(this, out go);
		public T FindSingle<T>() where T : Object => ReferencableGameObject.FindSingle<T>(this);
		public bool TryFindSingle<T>(out T o) where T : Object => ReferencableGameObject.TryFindSingle(this, out o);
		public List<GameObject> FindAll() => ReferencableGameObject.FindAll(this);
		public bool TryFindAll(out List<GameObject> objects) =>
			ReferencableGameObject.TryFindAll(this, out objects);
		public List<T> FindAll<T>() where T : Object => ReferencableGameObject.FindAll<T>(this);
	}
}
