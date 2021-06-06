using System;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Debugging;
using StubbUnity.StubbFramework.Scenes.Services;
using StubbUnity.StubbFramework.Time;

namespace StubbUnity.StubbFramework.Core
{
    public class StubbContext : IStubbContext
    {
        private EcsWorld _world;
        private IEcsDebug _debugger;
        
        protected EcsSystems RootSystems;

        public EcsFeature HeadFeature { get; set; }
        public EcsFeature MainFeature { get; set; }
        public EcsFeature TailFeature { get; set; }

        public bool IsDisposed => _world == null;
        public EcsWorld World => _world;

        public StubbContext() : this(new EcsWorld())
        { }

        public StubbContext(IEcsDebug debug) : this(new EcsWorld(), debug)
        { }

        public StubbContext(EcsWorld world, IEcsDebug debug = null)
        {
            Stubb.AddContext(this);
            
            _world = world;
            _debugger = debug;
            RootSystems = new EcsSystems(_world,  $"{GetType()}Systems");
            
            InitFeatures();
        }

        protected virtual void InitFeatures()
        {
            HeadFeature = new SystemHeadFeature(World);
            TailFeature = new SystemTailFeature(World);
        }
                
        /// <summary>
        /// Injects data globally (for root system and all child systems).
        /// </summary>
        public void Inject(object data, Type overridenType = null)
        {
            RootSystems.Inject(data, overridenType);
        }

        public void Init()
        {
            HeadFeature?.Init(RootSystems);
            MainFeature?.Init(RootSystems);
            TailFeature?.Init(RootSystems);

            _debugger?.Init(RootSystems, World);

            _InjectServices();

            RootSystems.ProcessInjects();
            RootSystems.Init();
        }

        public void Run()
        {
            RootSystems.Run();
            _debugger?.Debug();
        }

        public virtual void Dispose()
        {
            RootSystems.Destroy();
            RootSystems = null;
            _debugger = null;
            
            if (_world != null && _world.IsAlive())
            {
                _world.Destroy();
                _world = null;
            }
        }
        
        private void _InjectServices()
        {
            Inject(ServiceMapper<ITimeService>.Get());
            Inject(ServiceMapper<ISceneService>.Get());
        }
    }
}