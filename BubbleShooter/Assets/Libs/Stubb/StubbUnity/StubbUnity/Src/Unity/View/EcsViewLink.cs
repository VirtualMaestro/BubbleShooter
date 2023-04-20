using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.View;
using StubbUnity.StubbFramework.View.Components;
using StubbUnity.Unity.Pooling;
using UnityEngine;

namespace StubbUnity.Unity.View
{
    public class EcsViewLink : MonoBehaviour, IEcsViewLink, IPoolable
    {
        public bool hasPhysics;
       
        [HideInInspector]
        public bool disableInitializationPhase;
        
        private EcsEntity _entity = EcsEntity.Null;

        /// <summary>
        /// int number which represents type for an object.
        /// This type will be used for determination which object it is and for setting up collision pair.
        /// It determines if collision event will be sent during a collision of two objects.
        /// Default value 0, which means no collision events will be sent.
        /// </summary>
        public int TypeId { get; set; }
        
        public bool HasEntity => _entity != EcsEntity.Null && _entity.IsAlive();
        public string Name => gameObject.name;
        public bool IsDestroyed { get; private set; }
        public EcsWorld World { get; private set; }

        private void Awake()
        {
            World = Stubb.World;
            
            OnConstruct();
        }

        private void Start()
        {
            IsDestroyed = false;
            if (disableInitializationPhase) return;
            
            // create ECS binding
            _InitEntity();
            
            OnInitialize();
        }

        /// <summary>
        /// Init user's code here.
        /// Method is invoked on construct phase. Uses Awake phase: World is already created but entity not. 
        /// </summary>
        public virtual void OnConstruct()
        {
        }

        /// <summary>
        /// Init user's code here.
        /// Method is invoked on the init phase. Uses Start phase.
        /// Also this method will be invoked when object restores from the pool (if pool enable).
        /// </summary>
        public virtual void OnInitialize()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _InitEntity()
        {
            _entity = World.NewEntity();
            _entity.Get<EcsViewLinkComponent>().Value = this;
        }

        public ref EcsEntity GetEntity()
        {
            return ref _entity;
        }

        public void SetEntity(ref EcsEntity entity)
        {
            _entity = entity;
        }

        /// <summary>
        /// User's code implements here a logic that should be applied when game has to be set on Pause.
        /// </summary>
        public virtual void OnPause()
        {
        }

        /// <summary>
        /// User code implements here a logic that should be applied when game has to be resumed.
        /// </summary>
        public virtual void OnResume()
        {
        }

        /// <summary>
        /// Method is invoked when this object should be destroyed. 
        /// User code implements here a logic that should be applied when entity should be destroyed.
        /// By default it invokes when an object is put to a pool. In order to change this behaviour override 'OnToPool' method.
        /// </summary>
        protected virtual void OnDispose()
        {
            
        }
        
        /// <summary>
        /// Override for user's code. 
        /// Method is invoked when an object gets from pool.
        /// By default method invokes OnInitialize method. Override it to change behaviour. 
        /// </summary>
        protected virtual void OnGetFromPool()
        {
            OnInitialize();
        }

        /// <summary>
        /// Override for user's code.
        /// Method is invoked when an object puts to pool.
        /// By default method invokes OnDispose method. Override it to change behaviour. 
        /// </summary>
        protected virtual void OnPutToPool()
        {
            OnDispose();
        }
        
        /// <summary>
        /// For internal use.
        /// </summary>
        public void OnFromPool()
        {
            IsDestroyed = false;
            
            // create ECS binding
            _InitEntity();

            OnGetFromPool();
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        public void OnToPool()
        {
            OnPutToPool();
        }
        
        /// <summary>
        /// Destroys entity and GameObject. If 'HasPool' is true, a GameObject will be set to pool.
        /// </summary>
        public void Destroy()
        {
            if (IsDestroyed) return;
            IsDestroyed = true;

            if (HasEntity) 
                _entity.Destroy();
            
            if (TryGetComponent<PoolableMono>(out var poolableMono))
                poolableMono.Pool.Put(gameObject);                
            else if (gameObject != null)
            {
                OnDispose();
                Object.Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (IsDestroyed) return;
            IsDestroyed = true;

            if (HasEntity) 
                _entity.Destroy();
            
            OnDispose();
        }
    }
}