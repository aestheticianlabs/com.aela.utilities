using UnityEngine;

namespace AeLa.Utilities
{
	/// <summary>
	/// Component used to add editor comments
	/// </summary>
	public class EditorComment : MonoBehaviour
	{
#if UNITY_EDITOR
		[TextArea, SerializeField] private string comment;
#endif
	}
}