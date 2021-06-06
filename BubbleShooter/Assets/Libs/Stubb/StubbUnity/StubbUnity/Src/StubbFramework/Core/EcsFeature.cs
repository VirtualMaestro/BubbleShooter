using System;
using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Core
{
    public class EcsFeature : IEcsSystem
    {
        private bool _isEnable;
        private EcsSystems _parentSystems;
        private readonly EcsSystems _internalSystems;

        private readonly EcsWorld _world;

        /// <summary>
        /// For internal use.
        /// </summary>
        public EcsSystems InternalSystems => _internalSystems;

        public string Name { get; }
        public EcsWorld World => _world;

        public EcsFeature() : this(null)
        { }

        public EcsFeature(bool isEnable) : this(null, null, isEnable)
        { }
        
        public EcsFeature(EcsWorld world, bool isEnable) : this(world, null, isEnable)
        { }
        
        public EcsFeature(string name, bool isEnable) : this(null, name, isEnable)
        { }
        
        public EcsFeature(EcsWorld world, string name = null, bool isEnable = true)
        {
            _world = world ?? Stubb.World;
            Name = name ?? GetType().Name;
            _isEnable = isEnable;

            _internalSystems = new EcsSystems(_world, $"{Name}Systems");
        }

        public void Init(EcsSystems parentSystems)
        {
            _parentSystems = parentSystems;
            _parentSystems.Add(this);
            _parentSystems.Add(_internalSystems);

            if (!_isEnable)
                Enable = _isEnable;
        }

        public bool Enable
        {
            get => _isEnable;
            set
            {
                _isEnable = value;

                _EnableSystems(_internalSystems.Name, _isEnable);
            }
        }

        protected void Add(IEcsSystem system)
        {
            if (system is EcsFeature feature)
                feature.Init(_internalSystems);
            else
                _internalSystems.Add(system);
        }

        /// <summary>
        /// Injects only in scope of this feature (so all children systems).
        /// </summary>
        protected void Inject(object data, Type overridenType = null)
        {
            _internalSystems.Inject(data, overridenType);
        }

        protected void OneFrame<T>() where T : struct
        {
            _internalSystems.OneFrame<T>();
        }

        private void _EnableSystems(string systemsName, bool isEnable)
        {
            var idx = _parentSystems.GetNamedRunSystem(systemsName);
            _parentSystems.SetRunSystemState(idx, isEnable);
        }
    }
}