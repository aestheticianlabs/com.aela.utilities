using AeLa.Utilities.Physics;
using UnityEditor;

namespace AeLa.Utilities.Editor.Physics
{
	[CustomPropertyDrawer(typeof(DetectGround.Raycast))]
	public class DetectGroundRaycastPropertyDrawer : ForceExpandedPropertyDrawer
	{
		public DetectGroundRaycastPropertyDrawer() :
			base("layerMask", "ignoreTag", "distance", "orientToWorld", "offset")
		{
		}
	}
}