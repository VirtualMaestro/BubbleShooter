using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.View;
using StubbUnity.StubbFramework.View.Components;
using UnityEngine;

namespace StubbUnity.Unity.View
{
    public class EcsViewLink : MonoBehaviour, IEcsViewLink
    {
        public bool hasPhysics;
        [SerializeField] 
        private int typeId;
        
        private EcsEntity _entity = EcsEntity.Null;

        /// <summary>
        /// int number which represents type for an object.
        /// This type will be used for determination which object it is and for setting up collision pair.
        /// It determines if collision event will be sent during a collision of two objects.
        /// Default value 0, which means no collision events will be sent.
        /// </summary>
        public int TypeId
        {
            get => typeId;
            set => typeId = value;
        }
        
        public bool HasEntity => _entity != EcsEntity.Null && _entity.IsAlive();
        public string Name => gameObject.name;
        public bool IsDisposed { get; private set; }
        public EcsWorld World { get; private set; }

        private void Awake()
        {
            World = Stubb.World;
            IsDisposed = false;
            
            Construct();
        }

        private void Start()
        {
            // create ECS binding
            _InitEntity();

            Initialize();
        }

        /// <summary>
        /// Init user's code here.
        /// Uses Awake phase: World is already created but entity not. 
        /// </summary>
        public virtual void Construct()
        {
        }

        /// <summary>
        /// Init user's code here.
        /// Uses Start phase: everything has been initialized.
        /// </summary>
        public virtual void Initialize()
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
        /// User code implements here a logic that should be applied when game has to be set on Pause.
        /// </summary>
        public void OnPause()
        {
        }

        /// <summary>
        /// User code implements here a logic that should be applied when game has to be resumed.
        /// </summary>
        public void OnResume()
        {
        }

        /// <summary>
        /// Dispose entity and GameObject.
        /// Here should be user's custom dispose logic.
        /// IMPORTANT: in inherited classes should be base.Dispose() invoked. 
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;

            if (HasEntity) _entity.Destroy();
            if (gameObject != null) Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (IsDisposed) return;
            
            Dispose();
        }
    }
}