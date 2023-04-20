using StubbUnity.StubbFramework.Pooling;
using UnityEngine;

namespace StubbUnity.Unity.Pooling
{
    public static class PoolGameObjectExtension
    {
        public static GameObject Get(this IPool<GameObject> pool, Transform parent, bool worldPositionStays = true)
        {
            var instance = pool.Get();
            instance.transform.SetParent(parent, worldPositionStays);
            return instance;
        }
        
        public static GameObject Get(this IPool<GameObject> pool, in Vector3 position, in Quaternion rotation)
        {
            var instance = pool.Get();
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }
        
        public static GameObject Get(this IPool<GameObject> pool, in Vector3 position, in Quaternion rotation, Transform parent, bool worldPositionStays = true)
        {
            var instance = pool.Get();
            instance.transform.SetPositionAndRotation(position, rotation);
            instance.transform.SetParent(parent, worldPositionStays);
            return instance;
        }
        
        public static GameObject Get(this IPool<GameObject> pool, in Vector3 position)
        {
            var instance = pool.Get();
            instance.transform.position = position;
            return instance;
        }
        
        public static GameObject Get(this IPool<GameObject> pool, in Vector3 position, Transform parent, bool worldPositionStays = true)
        {
            var instance = pool.Get();
            instance.transform.position = position;
            instance.transform.SetParent(parent, worldPositionStays);
            return instance;
        }
        
        public static GameObject Get(this IPool<GameObject> pool, in Quaternion rotation)
        {
            var instance = pool.Get();
            instance.transform.rotation = rotation;
            return instance;
        }
        
        public static GameObject Get(this IPool<GameObject> pool, in Quaternion rotation, Transform parent, bool worldPositionStays = true)
        {
            var instance = pool.Get();
            instance.transform.rotation = rotation;
            instance.transform.SetParent(parent, worldPositionStays);
            return instance;
        }
    }
}