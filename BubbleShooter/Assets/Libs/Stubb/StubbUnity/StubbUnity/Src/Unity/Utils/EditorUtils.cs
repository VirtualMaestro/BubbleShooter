#if UNITY_EDITOR
using UnityEditor;

namespace StubbUnity.Unity.Utils
{
    public class EditorUtils
    {
        /// <summary>
        /// Get all instances of a specified type of the objects in Editor mode.
        /// </summary>
        /// <returns></returns>
        public static T[] GetAllInstances<T>(string searchPath = null) where T : UnityEngine.Object
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

            if (searchPath != null)
                guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { searchPath });

            var assets = new T[guids.Length];

            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return assets;
        }
    }
}
#endif