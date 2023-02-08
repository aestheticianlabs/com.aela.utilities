using NaughtyAttributes;
using UnityEngine;

namespace AeLa.Utilities.EditorTools
{
	/// <summary>
	/// Draws a custom gizmo for this object
	/// </summary>
	public class Gizmo : MonoBehaviour
	{
		public bool Persistent = true;

		[EnableIf("Persistent")]
		public Color PersistentColor = Color.magenta.WithAlpha(0.25f);

		public bool Selected = true;

		[EnableIf("Selected")]
		public Color SelectedColor = Color.magenta.WithAlpha(0.5f);

		public Mesh Mesh;
		public bool Wire = true;

		public Vector3 PositionOffset;
		public Vector3 Scale = Vector3.one;
		public float UniformScale = 1f;
		public Vector3 Rotation;
		public bool UseTransformMatrix = false;

		private void OnDrawGizmos()
		{
			if (!Persistent) return;
			Gizmos.color = PersistentColor;
			DrawGizmos();
		}

		private void OnDrawGizmosSelected()
		{
			if (!Selected) return;
			Gizmos.color = SelectedColor;
			DrawGizmos();
		}

		private void DrawGizmos()
		{
			var t = transform;
			var s = t.localScale;
			s.Scale(Scale * UniformScale);

			Gizmos.matrix = UseTransformMatrix
				? Matrix4x4.TRS(t.position, t.rotation * Quaternion.Euler(Rotation), s)
				: Matrix4x4.Scale(s);

			var position = PositionOffset;
			if (!UseTransformMatrix) position += transform.position;

			var rotation = UseTransformMatrix ? Quaternion.identity : Quaternion.Euler(Rotation);

			if (Wire) Gizmos.DrawWireMesh(Mesh, position, rotation);
			else Gizmos.DrawMesh(Mesh, position, rotation);
		}
	}
}