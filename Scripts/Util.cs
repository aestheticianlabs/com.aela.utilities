using UnityEngine;

namespace AeLa.Utilities
{
    public static partial class Util
    {
        // this sucks
        // source: https://forum.unity.com/threads/when-a-rigid-body-is-not-attached-component-getcomponent-rigidbody-returns-null-as-a-string.521633/#post-3423328
        /// <summary>
        /// Checks if an object either
        /// - is null
        /// - is a UnityEngine.Object that is == null, meaning that's invalid - ie. Destroyed, not assigned, or created with new
        ///
        /// Unity overloads the == operator for UnityEngine.Object, and returns true for a == null both if a is null, or if
        /// it doesn't exist in the c++ engine. This method is for checking for either of those being the case
        /// for objects that are not necessarily UnityEngine.Objects. This is useful when you're using interfaces, since ==
        /// is a static method, so if you check if a member of an interface == null, it will hit the default C# == check instead
        /// of the overridden Unity check.
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if the object is null, or if it's a UnityEngine.Object that has been destroyed</returns>
        public static bool IsNullOrUnityNull(object obj)
        {
            if (obj == null)
            {
                return true;
            }

            if (obj is Object)
            {
                if ((Object) obj == null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Activates a GameObject if it is inactive
        /// </summary>
        /// <param name="obj"></param>
        public static void ActivateIfInactive(GameObject obj)
        {
            if (!obj.activeSelf)
                obj.SetActive(true);
        }

        /// <summary>
        /// Deactivates a game object if it is active
        /// </summary>
        /// <param name="obj"></param>
        public static void DeactivateIfActive(GameObject obj)
        {
            if (obj.activeSelf)
                obj.SetActive(false);
        }

        public static GameObject Instantiate(GameObject prefab, bool keepPrefabName = true)
        {
            var inst = GameObject.Instantiate(prefab);
            if(keepPrefabName) inst.name = prefab.name;
            return inst;
        }
    }
}