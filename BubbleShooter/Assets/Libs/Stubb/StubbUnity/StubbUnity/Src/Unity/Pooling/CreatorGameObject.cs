using StubbUnity.StubbFramework.Pooling;
using UnityEngine;

namespace StubbUnity.Unity.Pooling
{
    public class CreatorGameObject : ICreator<GameObject>
    {
        private IPool<GameObject> _pool;
        private GameObject _prefab;
        private readonly Quaternion _rotation;
        private readonly Vector3 _scale;
        
        public CreatorGameObject(GameObject prefab, IPool<GameObject> pool)
        {
            _pool = pool;
            _prefab = Object.Instantiate(prefab);
            _prefab.AddComponent<PoolableMono>().Pool = _pool;
            _prefab.SetActive(false);
            _rotation = _prefab.transform.rotation;
            _scale = _prefab.transform.localScale;

            Object.DontDestroyOnLoad(_prefab);
        }

        public GameObject OnCreate()
        {
            var instance = Object.Instantiate(_prefab);
            instance.GetComponent<PoolableMono>().Pool = _pool;
            instance.SetActive(true);
            
            return instance;
        }
         
        public void OnToPool(GameObject instance)
        {
            instance.SetActive(false);
            instance.transform.SetParent(null);

            if (!instance.TryGetComponent<PoolableMono>(out var poolableMono))
            {
                poolableMono = instance.AddComponent<PoolableMono>();
                poolableMono.Pool = _pool;
            }
            
            poolableMono.OnToPool();
        }
        
        public void OnFromPool(GameObject instance)
        {
            instance.transform.rotation = _rotation;
            instance.transform.localScale = _scale;
            instance.GetComponent<PoolableMono>().OnFromPool();
            instance.SetActive(true);
        }

        public void OnDispose(GameObject instance)
        {
            if (instance != null)
                Object.Destroy(instance);
        }

        public void Dispose()
        {
            Object.Destroy(_prefab);
            _prefab = default;
            _pool = null;
        }
    }
}