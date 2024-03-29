using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Core.Events;
using StubbUnity.StubbFramework.Debugging;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.StubbFramework.Physics;
using StubbUnity.StubbFramework.Time;
using StubbUnity.Unity.Debugging;
using StubbUnity.Unity.Logging;
using StubbUnity.Unity.Scenes;
using UnityEngine;

namespace StubbUnity.Unity
{
    public class EntryPoint : MonoBehaviour
    {
        private IStubbContext _context;
        private IPhysicsContext _physicsContext;
        private bool _hasFocus = true;
        private bool _isPaused;

        [Tooltip("Enable UI events emitter to provide ui events to ecs tier")]
        public bool enableUiEmitter;
        public EcsWorld World => _context.World;
        public IEcsDebug Debug { get; private set; }

        private void Awake()
        {
            log.AddAppender(UnityLogAppender.LogDelegate);

            Debug = CreateDebug();
            _context = CreateContext();
            _physicsContext = CreatePhysicsContext();

            MapServices(_context);
            OnConstruct(_context);

            if (!enableUiEmitter) return;
            
            var emitter = gameObject.GetComponent<EcsUiEmitter>();
            
            if (emitter != null)
                _context.MainFeature.InternalSystems.InjectUi(emitter);
        }

        private void Start()
        {
            OnInitialize(_context);
            
            _context.Init();
            _physicsContext?.Init();
            
            DontDestroyOnLoad(gameObject);
            
            OnPostInitialize(_context);
        }

        /// <summary>
        /// Have to be overriden by user for main feature or for all (Head, Main, Tail).
        /// It is called in the Awake phase before context and systems were initialized.
        /// </summary>
        protected virtual void OnConstruct(IStubbContext context)
        {
            
        }

        /// <summary>
        /// It is called in the Start phase before context and system were initialized and share data injected.
        /// It is used if some data should be injected or any other initializations.
        /// </summary>
        protected virtual void OnInitialize(IStubbContext context)
        {
            
        }
        
        /// <summary>
        /// It is called in the Start phase after context and all systems were initialized and share data injected,
        /// but before the first update invocation.
        /// </summary>
        protected virtual void OnPostInitialize(IStubbContext context)
        {
            
        }
        
        /// <summary>
        /// Override if need custom context. 
        /// </summary>
        protected virtual IStubbContext CreateContext()
        {
            return new StubbContext(Debug);
        }

        /// <summary>
        /// Override if need custom Debug. 
        /// </summary>
        protected virtual IEcsDebug CreateDebug()
        {
            return new UnityEcsDebug();
        }

        /// <summary>
        /// Override if need physics context. 
        /// </summary>
        protected virtual IPhysicsContext CreatePhysicsContext()
        {
            return default;
        }
        
        /// <summary>
        /// Maps Unity specific services.
        /// </summary>
        protected void MapServices(IStubbContext context)
        {
            context.Inject(new SceneService());
            context.Inject(new TimeService());
        }

        private void Update()
        {
            _context.Run();
        }

        private void FixedUpdate()
        {
            _physicsContext?.Run();
        }

        private void OnDestroy()
        {
            _context.Destroy();
            _physicsContext?.Destroy();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus == _hasFocus) return;

            _hasFocus = hasFocus;
            if (_hasFocus)
                World.NewEntity().Get<ApplicationFocusOnEvent>();
            else
                World.NewEntity().Get<ApplicationFocusOffEvent>();
        }

        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused == _isPaused) return;

            _isPaused = isPaused;
            if (_isPaused)
                World.NewEntity().Get<ApplicationPauseOnEvent>();
            else
                World.NewEntity().Get<ApplicationPauseOffEvent>();
        }

        private void OnApplicationQuit()
        {
            World.NewEntity().Get<ApplicationQuitEvent>();
        }
    }
}