using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Pooling
{
    public static class GameObjectPoolingExtension
    {
        public static void Destroy(this GameObject instance)
        {
            if (instance.TryGetComponent<IEcsViewLink>(out var viewLink))
                viewLink.Destroy();
            else if (instance.TryGetComponent<PoolableMono>(out var poolableMono) && poolableMono.Pool != null)
                poolableMono.Pool.Put(instance);
            else 
                Object.Destroy(instance);
        }
    }
}