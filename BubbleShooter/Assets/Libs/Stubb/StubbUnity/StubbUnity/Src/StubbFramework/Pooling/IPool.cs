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
    /// Processes:
    /// 1. How an instance is created.
    /// 2. How an instance is put to a pool.
    /// 3. How an instance is got from a pool.
    /// 4. How an instance is disposed.
    /// </summary>
    public interface ICreator<T> : IDisposable
    {
        /// <summary>
        /// It is invoked when an instance of a poolable item is going to be created.
        /// </summary>
        T OnCreate();
        
        /// <summary>
        /// It is invoked when an item is put in a pool.
        /// </summary>
        void OnToPool(T t);
        
        /// <summary>
        /// It is invoked when an item is taken from a pool.
        /// </summary>
        void OnFromPool(T t);

        /// <summary>
        /// It is invoked when an item gets destroyed while clearing a pool.
        /// </summary>
        /// <param name="t"></param>
        void OnDispose(T t);
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
        /// Returns number of free slots still available in the pool.
        /// </summary>
        int FreeSlots { get; }
        
        /// <summary>
        /// Returns number of available free-to-use items in the pool.
        /// </summary>
        int AvailableItems { get; }
        
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
        event Action<IPoolGeneric, Type> OnRemove;
    }
}