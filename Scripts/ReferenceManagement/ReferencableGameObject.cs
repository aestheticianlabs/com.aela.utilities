using UnityEngine;

namespace AeLa.Utilities.ReferenceManagement
{
	[DefaultExecutionOrder(-99)]
	public partial class ReferencableGameObject : MonoBehaviour
	{
		[SerializeField] private GameObjectReference reference;
		public GameObjectReference Reference => reference;

		private void Awake()
		{
			InitializeReference(reference);
			
			if (!references.TryGetValue(reference, out var refs))
			{
				references.Add(reference, new() { gameObject });
				return;
			}

			if (reference.IsUnique && refs.Count > 0)
			{
				Debug.LogWarning($"More than one object for unique reference: {reference}");

				if (reference.ReplaceExisting)
				{
					Debug.LogWarning($"Replacing existing reference object: {refs[0]}");
					var old = refs[0];
					Destroy(old);
					refs[0] = gameObject;
				}
				else
				{
					Debug.LogWarning($"Destroying self instead of replacing object: {refs[0]}");
					Destroy(gameObject);
				}
			}
			else
			{
				refs.Add(gameObject);
			}
		}

		private void OnDestroy()
		{
			if (references.TryGetValue(reference, out var refs) && refs.Contains(gameObject))
			{
				refs.Remove(gameObject);
			}
		}
	}
}