using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Extensions
{
    public static class GameObjectExtension
    {
        public static bool HasComponent<T>(this GameObject gameObject) where T : class
        {
            return gameObject.GetComponent<T>() != null;
        }

        /// <summary>
        /// Returns true if GameObject contains component of IEcsViewLink
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool HasViewLink(this GameObject gameObject)
        {
            return gameObject.TryGetComponent<IEcsViewLink>(out _);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Add/remove component to/from gameobject.
        /// This functionality is used for editor.
        /// </summary>
        public static void EnableComponent<T>(this GameObject gameObject, bool isEnable, bool hideInInspector = false) where T: MonoBehaviour
        {
            if (isEnable)
            {
                if (gameObject.HasComponent<T>()) 
                    return;
                
                var comp = gameObject.AddComponent<T>();
                if (hideInInspector)
                    comp.hideFlags = HideFlags.HideInInspector;
            }
            else if (gameObject.HasComponent<T>())
                Object.DestroyImmediate(gameObject.GetComponent<T>());
        }
#endif
    }
}