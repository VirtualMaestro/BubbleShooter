using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace StubbUnity.StubbFramework.Pooling
{
    public partial class Pools
    {
        #region Singleton

        private static Pools _instance;
        public static Pools I => _instance ?? (_instance = new Pools());

        #endregion

        private readonly Dictionary<Type, IPoolGeneric> _pools;

        private Pools()
        {
            _pools = new Dictionary<Type, IPoolGeneric>();
        }

        /// <summary>
        /// Gets or creates and returns a pool with a given type.
        /// </summary>
        /// <param name="capacity">Initial capacity of a pool.</param>
        /// <param name="prewarm">If 'true' will be created the number of instances equal to the 'capacity' of the pool.</param>
        public IPool<T> Get<T>(int capacity = 5, bool prewarm = false)
        {
            var pool = _Create<T>(capacity);

            if (pool.Creator == null && pool.CreateMethod == null)
                pool.CreateMethod = () => (T) Activator.CreateInstance(typeof(T), true);

            if (prewarm)
                pool.PreWarm(pool.Size);

            return pool;
        }

        /// <summary>
        /// Gets or creates and returns a pool with a given type.
        /// </summary>
        /// <param name="capacity">Initial capacity of the pool.</param>
        /// <param name="creator">Instance of ICreator which will be used for creating an instance of pool's type.</param>
        /// <param name="prewarm">If 'true' will be created the number of instances equal to the 'capacity' of the pool.</param>
        public IPool<T> Get<T>(int capacity, ICreator<T> creator, bool prewarm = false)
        {
            var pool = _Create<T>(capacity);
            pool.Creator = creator;

            if (prewarm)
                pool.PreWarm(pool.Size);

            return pool;
        }

        /// <summary>
        /// Gets or creates and returns a pool with a given type.
        /// </summary>
        /// <param name="capacity">Initial capacity of the pool.</param>
        /// <param name="createMethod">Method which will be used for creating an instance of pool's type.</param>
        /// <param name="prewarm">If 'true' will be created the number of instances equal to the 'capacity' of the pool.</param>
        public IPool<T> Get<T>(int capacity, Func<T> createMethod, bool prewarm = false)
        {
            var pool = _Create<T>(capacity);
            pool.CreateMethod = createMethod;

            if (prewarm)
                pool.PreWarm(pool.Size);

            return pool;
        }

        /// <summary>
        /// Clears all pools.
        /// </summary>
        /// <param name="shrink">if 'true' the pools will be shrunk</param>
        public void ClearAll(bool shrink = false)
        {
            foreach (var pair in _pools)
            {
                pair.Value.Clear(shrink);
            }
        }

        /// <summary>
        /// Disposes all pools.
        /// </summary>
        public void DisposeAll()
        {
            foreach (var pair in _pools)
            {
                pair.Value.OnRemove -= _OnRemovePoolHandler;
                pair.Value.Dispose();
            }

            _pools.Clear();
        }

        public bool Has<T>()
        {
            return _pools.ContainsKey(typeof(T));
        }
        
        /// <summary>
        /// Creates a pool with given type.
        /// </summary>
        /// <param name="capacity">Initial capacity of the pool. Min value is 5.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IPool<T> _Create<T>(int capacity = 5)
        {
            if (Has<T>())
                return (Pool<T>) _pools[typeof(T)];

            var pool = new Pool<T>(capacity);
            pool.OnRemove += _OnRemovePoolHandler;
            _pools[typeof(T)] = pool;

            return pool;
        }
        
        private void _OnRemovePoolHandler(IPoolGeneric sender, Type type)
        {
            _pools.Remove(type);
        }
    }
}