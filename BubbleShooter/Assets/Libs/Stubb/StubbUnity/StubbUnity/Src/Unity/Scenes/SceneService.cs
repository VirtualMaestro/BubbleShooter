using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        
        public void Process(ProcessSetScenesConfig config)
        {
            _processingQueue.Enqueue(config);
            if (_currentConfig != null) return;

            _ProcessNextConfig();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _ProcessNextConfig()
        {
            if (_processingQueue.Count == 0) return;

            SceneManager.sceneLoaded += _OnSceneLoaded;
            SceneManager.sceneUnloaded += _OnSceneUnloaded;

            _currentConfig = _processingQueue.Dequeue();
            _CollectAllScenesOnStage();

            if (_currentConfig.LoadingList.Count > 0)
                _Load();
            else
                _Unload();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _CollectAllScenesOnStage()
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
                _activeScenes.Add(SceneManager.GetSceneAt(i));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _Load()
        {
            var loadingScenes = _currentConfig.LoadingList;

            foreach (var config in loadingScenes)
                SceneManager.LoadSceneAsync(config.Name.FullName, LoadSceneMode.Additive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _Unload()
        {
            if (_currentConfig.UnloadOthers)
                _UnloadOthers();
            else if (_currentConfig.UnloadingList.Count > 0)
                _UnloadList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _UnloadOthers()
        {
            _numScenesToUnload = _activeScenes.Count;

            foreach (var scene in _activeScenes)
            {
                if (scene.GetController(out var controller) && ((SceneController) controller).dontUnload) continue;
                
                SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _UnloadList()
        {
            var sceneNames = _currentConfig.UnloadingList;
            _numScenesToUnload = sceneNames.Count;

            foreach (var sceneName in sceneNames)
            {
                var sceneIndex = _activeScenes.FindIndex(activeScene => activeScene.isLoaded && activeScene.IsNameEqual(sceneName));
                if (sceneIndex < 0) continue;
                
                var scene = _activeScenes[sceneIndex];
                SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
                _activeScenes.RemoveAt(sceneIndex);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _AllScenesLoaded()
        {
            foreach (var sceneKeyValue in _loadedConfigs)
            {
                if (!sceneKeyValue.Value.GetController(out var sceneController)) continue;
                
                var config = sceneKeyValue.Key;

                if (config.IsMain)
                    sceneController.SetAsMain();
    
                if (config.IsActive)
                    sceneController.ShowContent();
            }

            Stubb.World.NewEntity().Get<ScenesSetLoadingCompleteEvent>().ScenesSetName = _currentConfig.Name;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _SceneSetProcessingComplete()
        {
            SceneManager.sceneLoaded -= _OnSceneLoaded;
            SceneManager.sceneUnloaded -= _OnSceneUnloaded;

            _loadedConfigs.Clear();
            _activeScenes.Clear();

            _numScenesToUnload = 0;
            _currentConfig = null;

            _ProcessNextConfig();
        }
        
        private void _OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
            
            Stubb.World.NewEntity().Get<SceneLoadingCompleteEvent>().SceneName = scene.path;
        }
        
        private void _OnSceneUnloaded(Scene scene)
        {
            --_numScenesToUnload;

            Stubb.World.NewEntity().Get<SceneUnloadingCompleteEvent>().SceneName = scene.path;

            if (_numScenesToUnload == 0)
            {
                Stubb.World.NewEntity().Get<ScenesSetUnloadingCompleteEvent>().ScenesSetName = _currentConfig.Name;
                _SceneSetProcessingComplete();
            }
        }
    }
}