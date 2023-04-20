using System;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.StubbFramework.Scenes;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.StubbFramework.Scenes.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity.Unity.Scenes
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        public bool hasPooling;
        public bool dontUnload;
        
        [SerializeField] 
        private GameObject content;
        private Scene _scene;
        private EcsEntity _entity = EcsEntity.Null;
        private bool _shouldBeShown = true;
        
        protected EcsWorld World { get; private set; }
        protected GameObject Content => content;

        public Scene Scene => _scene;
        public IAssetName SceneName { get; private set; }
        public bool IsContentActive => content.activeSelf;
        public bool IsMain => SceneManager.GetActiveScene() == _scene;
        public bool HasEntity => _entity != EcsEntity.Null && _entity.IsAlive();
        public bool IsDestroyed { get; private set; }

        private void Awake()
        {
            World = Stubb.World;
            IsDestroyed = false;
            _scene = gameObject.scene;
            SceneName = new SceneName(_scene.name, _scene.path);
        }

        private void Start()
        {
            if (content == null)
                throw new Exception($"Content wasn't set for the controller of the scene '{SceneName}'!");

            // create ECS binding
            _InitEntity();

            if (_shouldBeShown)
                _Show();
            else
                _Hide();
            
            OnInitialize();
        }

        private void _InitEntity()
        {
            _entity = World.NewEntity();
            _entity.Get<SceneComponent>().Scene = this;
            
            // sends create event 
            World.NewEntity().Get<SceneCreatedEvent>().Scene = this;
        }

        /// <summary>
        /// Init user's code here.
        /// </summary>
        public virtual void OnInitialize()
        {
        }

        public void SetAsMain()
        {
            SceneManager.SetActiveScene(_scene);
        }

        public void ShowContent()
        {
            _shouldBeShown = true;
            if (IsDestroyed || IsContentActive || !HasEntity) return;

            _Show();
        }

        private void _Show()
        {
            if (content != null)
                content.SetActive(true);
            
            if (_entity.Has<IsSceneInactiveComponent>())
                _entity.Del<IsSceneInactiveComponent>();

            _entity.Get<IsSceneActiveComponent>();
            
            // sends event scene has been activated
            World.NewEntity().Get<SceneActivatedEvent>().Scene = this;
        }

        public void HideContent()
        {
            _shouldBeShown = false;
            
            if (IsDestroyed || !IsContentActive || !HasEntity) return;
            
            _Hide();
        }

        private void _Hide()
        {
            if (content != null)
                content.SetActive(false);
            
            if (_entity.Has<IsSceneActiveComponent>())
                _entity.Del<IsSceneActiveComponent>();
                
            _entity.Get<IsSceneInactiveComponent>();
            
            // sends event scene has been deactivated
            World.NewEntity().Get<SceneDeactivatedEvent>().Scene = this;
        }

        public void SetEntity(ref EcsEntity entity)
        {
            _entity = entity;
        }

        public ref EcsEntity GetEntity()
        {
            return ref _entity;
        }

        /// <summary>
        /// Method is invoked when scene is unloading.
        /// Custom user's code should be here.
        /// For unloading scene use World.UnloadScene(s).
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        private void OnDestroy()
        {
            if (IsDestroyed)
            {
                log.Warn($"SceneController.Destroy. Controller with scene '{SceneName.FullName}' is already destroyed!");
                return;
            }
            
            _Hide();
            
            OnDispose();
            
            if (HasEntity)
                _entity.Destroy();
            
            // sends destroy event
            World.NewEntity().Get<SceneDestroyedEvent>().SceneName = SceneName;
            
            IsDestroyed = true;
        }
    }
}