using System;
using System.Runtime.CompilerServices;
using StubbUnity.StubbFramework.Pooling;
using UnityEngine;

namespace StubbUnity.Unity.Pooling
{
    [DisallowMultipleComponent]
    public sealed class PoolsController : MonoBehaviour
    {
        [SerializeField] 
        private PoolItem[] poolPrefabs;
        private IPool<GameObject>[] _pools;

        private void Awake()
        {
            _PreparePools();
        }

        private void OnDestroy()
        {
            _DestroyPools();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _PreparePools()
        {
            if (poolPrefabs == null || poolPrefabs.Length == 0) return;
            
            _pools = new IPool<GameObject>[poolPrefabs.Length];
            var i = 0;
            
            foreach (var poolItem in poolPrefabs)
            {
                if (!Pools.I.Has(poolItem.prefab))
                    _pools[i++] = Pools.I.Get(poolItem.prefab, poolItem.prewarmCount, true);
            }    
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _DestroyPools()
        {
            if (_pools == null) return;
            
            foreach (var pool in _pools)
                pool.Dispose();

            _pools = null;
            poolPrefabs = null;
        }
    }
        
    [Serializable]
    internal class PoolItem
    {
        public GameObject prefab;
        
        [Min(1)]
        public int prewarmCount;
    }
}