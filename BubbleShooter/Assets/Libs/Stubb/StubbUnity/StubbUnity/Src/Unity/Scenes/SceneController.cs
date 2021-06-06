using System;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.StubbFramework.Scenes;
using StubbUnity.StubbFramework.Scenes.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StubbUnity.Unity.Scenes
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        [SerializeField] private GameObject content;
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
        public bool IsDisposed { get; private set; }

        private void Awake()
        {
            World = Stubb.World;
            IsDisposed = false;
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
            
            Initialize();
        }

        private void _InitEntity()
        {
            _entity = World.NewEntity();
            _entity.Get<SceneComponent>().Scene = this;
            _entity.Get<SceneReadyComponent>();
        }

        /// <summary>
        /// Init user's code here.
        /// </summary>
        public virtual void Initialize()
        {
        }

        public void SetAsMain()
        {
            SceneManager.SetActiveScene(_scene);
        }

        public void ShowContent()
        {
            _shouldBeShown = true;
            if (IsDisposed || IsContentActive || !HasEntity) return;

            _Show();
        }

        private void _Show()
        {
            content.SetActive(true);
            
            if (_entity.Has<IsSceneInactiveComponent>())
                _entity.Del<IsSceneInactiveComponent>();

            _entity.Get<IsSceneActiveComponent>();
            _entity.Get<SceneBecomeActiveComponent>();
        }

        public void HideContent()
        {
            _shouldBeShown = false;
            
            if (IsDisposed || !IsContentActive || !HasEntity) return;
            
            _Hide();
        }

        private void _Hide()
        {
            content.SetActive(false);
            
            if (_entity.Has<IsSceneActiveComponent>())
                _entity.Del<IsSceneActiveComponent>();
                
            _entity.Get<IsSceneInactiveComponent>();
            _entity.Get<SceneBecomeInactiveComponent>();
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
        /// Custom user's code should be here.
        /// For unloading scene use World.UnloadScene(s).
        /// </summary>
        public virtual void Dispose()
        {
        }

        private void OnDestroy()
        {
            if (IsDisposed)
            {
                log.Warn($"SceneController.Destroy. Controller with scene '{SceneName.FullName}' is already destroyed!");
                return;
            }
            
            IsDisposed = true;
            
            Dispose();
            
            if (HasEntity)
                _entity.Destroy();
        }
    }
}