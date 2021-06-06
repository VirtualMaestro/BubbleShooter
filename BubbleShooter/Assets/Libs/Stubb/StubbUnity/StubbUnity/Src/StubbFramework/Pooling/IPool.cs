using System;

namespace StubbUnity.StubbFramework.Pooling
{
    public interface IPool<T> : IPoolGeneric
    {
        /// <summary>
        /// Set create method which will create new instances.
        /// </summary>
        Func<T> CreateMethod { set; get; }
        /// <summary>
        /// Set creator for the strategy of creating, putting and getting instances.
        /// </summary>
        ICreator<T> Creator { set; get; }
        /// <summary>
        /// Get or create item from the pool.
        /// </summary>
        T Get();
        /// <summary>
        /// Store item in the pool.
        /// </summary>
        void Put(T t);
    }
    
    /// <summary>
    /// Responsibilities:
    /// 1. How the instance of the stored type should be created, when the pool doesn't have enough instances.
    /// 2. How instance should be properly initialized during getting from the pool.
    /// 3. How instance should be properly deactivated during storing it in a pool. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICreator<T> : IDisposable
    {
        /// <summary>
        /// Invokes when instance of poolable item has to be created.
        /// </summary>
        T Create();
        /// <summary>
        /// Invokes when item is going to be stored in the pool.
        /// </summary>
        void BeforeStore(T t);
        /// <summary>
        /// Invokes when item is going to be got from the pool.
        /// </summary>
        void AfterRestore(T t);
    }
    
    public interface IPoolGeneric
    {
        /// <summary>
        /// Check if pool empty.
        /// It can be also used to know if an instance that was returned by 'Get' is newly created or got from the pool.
        /// var isItemNewlyCreated = pool.IsEmpty;
        /// var item = pool.Get();
        /// </summary>
        bool IsEmpty { get; }
        /// <summary>
        /// Check if the pool is full.
        /// </summary>
        bool IsFull { get; }
        /// <summary>
        /// Check if the pool is destroyed.
        /// </summary>
        bool IsDisposed { get; }
        /// <summary>
        /// Returns number of how many slots still available in the pool.
        /// </summary>
        int Available { get; }
        /// <summary>
        /// Total pool size.
        /// </summary>
        int Size { get; }
        /// <summary>
        /// Pre-creates instances for all available slots in the pool.
        /// </summary>
        void PreWarm();
        /// <summary>
        /// Pre-creates the given number of the instances in the pool.
        /// </summary>
        void PreWarm(int count);
        /// <summary>
        /// Clear all the pool.
        /// </summary>
        /// <param name="shrink">if 'true' the pool will be shrunk to the 'initialCapacity'. </param>
        void Clear(bool shrink = false);
        /// <summary>
        /// Dispose the pool. After this the pool can't be used anymore.
        /// </summary>
        void Dispose();
        /// <summary>
        /// The event is sent before the pool is disposed of.
        /// </summary>
        event RemovePool OnRemove;
    }

    public delegate void RemovePool(IPoolGeneric pool, Type type);
}