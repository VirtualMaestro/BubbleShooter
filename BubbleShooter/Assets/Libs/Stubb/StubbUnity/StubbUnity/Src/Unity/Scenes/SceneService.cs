using System.Collections.Generic;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Scenes.Configurations;
using StubbUnity.StubbFramework.Scenes.Events;
using StubbUnity.StubbFramework.Scenes.Services;
using StubbUnity.Unity.Extensions;
using UnityEngine.SceneManagement;

namespace StubbUnity.Unity.Scenes
{
    public class SceneService : ISceneService
    {
        private int _numScenesToUnload;
        private readonly List<Scene> _activeScenes = new List<Scene>();

        private readonly List<KeyValuePair<ILoadingSceneConfig, Scene>> _loadedConfigs =
            new List<KeyValuePair<ILoadingSceneConfig, Scene>>();

        private readonly Queue<ProcessSetScenesConfig> _processingQueue = new Queue<ProcessSetScenesConfig>();
        private ProcessSetScenesConfig _currentConfig;

        public void Process(ProcessSetScenesConfig config)
        {
            _processingQueue.Enqueue(config);
            if (_currentConfig != null) return;

            _ProcessNextConfig();
        }

        private void _ProcessNextConfig()
        {
            if (_processingQueue.Count == 0) return;

            SceneManager.sceneLoaded += _SceneLoaded;
            SceneManager.sceneUnloaded += _SceneUnloaded;

            _currentConfig = _processingQueue.Dequeue();
            _CollectAllScenesOnStage();

            if (_currentConfig.LoadingList.Count > 0)
                _Load();
            else
                _Unload();
        }

        private void _CollectAllScenesOnStage()
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                _activeScenes.Add(SceneManager.GetSceneAt(i));
            }
        }

        private void _Load()
        {
            var loadingScenes = _currentConfig.LoadingList;

            foreach (var config in loadingScenes)
                SceneManager.LoadSceneAsync(config.Name.FullName, LoadSceneMode.Additive);
        }

        private void _Unload()
        {
            if (_currentConfig.UnloadOthers)
                _UnloadOthers();
            else if (_currentConfig.UnloadingList.Count > 0)
                _UnloadList();
        }

        private void _SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var loadedConfig = _currentConfig.RemoveFromLoadingList(scene.path);
            _loadedConfigs.Add(new KeyValuePair<ILoadingSceneConfig, Scene>(loadedConfig, scene));

            if (_currentConfig.IsEmpty)
            {
                _AllScenesLoaded();

                if (_currentConfig.HasUnloading)
                    _Unload();
                else
                    _SceneSetProcessingComplete();
            }
        }

        private void _UnloadOthers()
        {
            _numScenesToUnload = _activeScenes.Count;

            foreach (var scene in _activeScenes)
                SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        private void _UnloadList()
        {
            var sceneNames = _currentConfig.UnloadingList;
            _numScenesToUnload = sceneNames.Count;

            foreach (var sceneName in sceneNames)
            {
                var sceneIndex = _activeScenes.FindIndex(activeScene => activeScene.IsNameEqual(sceneName));
                var scene = _activeScenes[sceneIndex];
                SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                _activeScenes.RemoveAt(sceneIndex);
            }
        }

        private void _SceneUnloaded(Scene scene)
        {
            --_numScenesToUnload;

            Stubb.World.NewEntity().Get<SceneUnloadingCompleteEvent>().SceneName = scene.path;

            if (_numScenesToUnload == 0)
            {
                Stubb.World.NewEntity().Get<ScenesSetUnloadingCompleteEvent>().ScenesSetName = _currentConfig.Name;
                _SceneSetProcessingComplete();
            }
        }

        private void _AllScenesLoaded()
        {
            foreach (var scenes in _loadedConfigs)
            {
                var config = scenes.Key;
                var scene = scenes.Value;
                var sceneController = scene.GetController();

                if (config.IsMain)
                    sceneController?.SetAsMain();

                if (config.IsActive)
                    sceneController?.ShowContent();
            }

            Stubb.World.NewEntity().Get<ScenesSetLoadingCompleteEvent>().ScenesSetName = _currentConfig.Name;
        }

        private void _SceneSetProcessingComplete()
        {
            SceneManager.sceneLoaded -= _SceneLoaded;
            SceneManager.sceneUnloaded -= _SceneUnloaded;

            _loadedConfigs.Clear();
            _activeScenes.Clear();

            _numScenesToUnload = 0;
            _currentConfig = null;

            _ProcessNextConfig();
        }

        public bool HasScene(in IAssetName sceneName)
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if (sceneName.FullName.Equals(scene.path))
                    return true;
            }

            return false;
        }
    }
}