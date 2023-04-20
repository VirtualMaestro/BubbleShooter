using System.Collections.Generic;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Scenes.Configurations;
using StubbUnity.StubbFramework.Scenes.Events;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class SceneWorldExtension
    {
        /// <summary>
        /// Loads a scene by a name.
        /// It implies that scene will be activated immediately, and will be main. 
        /// </summary>
        public static void LoadScene(this EcsWorld world, IAssetName sceneName, object payload = null)
        {
            var config = new LoadingSceneConfig {Name = sceneName, IsActive = true, IsMain = true, Payload = payload};
            var list = new List<ILoadingSceneConfig> {config};

            LoadScenes(world, list);
        }

        /// <summary>
        /// Loads a scene by a name.
        /// It implies that scene will be activated immediately, and will be main. 
        /// </summary>
        public static void LoadScene(this EcsWorld world, IAssetName sceneName, IAssetName unloadScene, object payload = null)
        {
            var loadingList = new List<ILoadingSceneConfig> {new LoadingSceneConfig {Name = sceneName, IsActive = true, IsMain = true, Payload = payload}};
            var unloadingList = new List<IAssetName> {unloadScene};

            LoadScenes(world, loadingList, unloadingList);
        }
        
        /// <summary>
        /// Loads a scene by a name and unload all others scene if 'unloadOthers' is true.
        /// It implies that scene will be activated immediately, and will be main. 
        /// </summary>
        public static void LoadScene(this EcsWorld world, IAssetName sceneName, bool unloadOthers, object payload = null)
        {
            var loadingList = new List<ILoadingSceneConfig> {new LoadingSceneConfig {Name = sceneName, IsActive = true, IsMain = true, Payload = payload}};

            LoadScenes(world, loadingList, unloadOthers);
        }

        /// <summary>
        /// Loads a scene by a config.  
        /// </summary>
        public static void LoadScene(this EcsWorld world, ILoadingSceneConfig config, string configName = null)
        {
            var list = new List<ILoadingSceneConfig> {config};
            LoadScenes(world, list, configName);
        }

        /// <summary>
        /// Loads a scene by a config and unload scene with a given name.  
        /// </summary>
        public static void LoadScene(this EcsWorld world, ILoadingSceneConfig config, IAssetName unloadScene,
            string configName = null)
        {
            var loadList = new List<ILoadingSceneConfig> {config};
            var unloadList = new List<IAssetName> {unloadScene};
            LoadScenes(world, loadList, unloadList, configName);
        }

        /// <summary>
        /// Loads a scene by a config and unload scene with a given names.  
        /// </summary>
        public static void LoadScene(this EcsWorld world, ILoadingSceneConfig config, List<IAssetName> unloadScenes,
            string configName = null)
        {
            var list = new List<ILoadingSceneConfig> {config};
            LoadScenes(world, list, unloadScenes, configName);
        }

        /// <summary>
        /// Loads a scene by a config and unload others scenes.  
        /// </summary>
        public static void LoadScene(this EcsWorld world, ILoadingSceneConfig config, bool unloadOthers,
            string configName = null)
        {
            var list = new List<ILoadingSceneConfig> {config};
            LoadScenes(world, list, unloadOthers, configName);
        }

        /// <summary>
        /// Add configuration of the scenes list to load.
        /// LoadScenesComponent will be sent.
        /// </summary>
        /// <param name="world"> Extension to the EcsWorld</param>
        /// <param name="configs">List of the ILoadingSceneConfig to load</param>
        /// <param name="configName">When bunch of the scenes will be loaded, system will be notified with 'ScenesLoadComplete' component and the only field inside is this configName param, so it is possible to identify which set of scenes was loaded.</param>
        public static void LoadScenes(this EcsWorld world, List<ILoadingSceneConfig> configs, string configName = null)
        {
            ref var loadScenes = ref world.NewEntity().Get<ProcessScenesEvent>();
            loadScenes.Name = configName;
            loadScenes.LoadingScenes = configs;
            loadScenes.UnloadingScenes = null;
            loadScenes.UnloadOthers = false;
        }

        /// <summary>
        /// Add configuration of the scenes list to load.
        /// LoadScenesComponent will be sent.
        /// </summary>
        /// <param name="world"> Extension to the EcsWorld</param>
        /// <param name="configs">List of the ILoadingSceneConfig to load</param>
        /// <param name="unloadScenes">scenes names which have to unload after given list config of new scenes will be loaded.</param>
        /// <param name="configName">When bunch of the scenes will be loaded, system will be notified with 'ScenesLoadComplete' component and the only field inside is this configName param, so it is possible to identify which set of scenes was loaded.</param>
        public static void LoadScenes(this EcsWorld world, List<ILoadingSceneConfig> configs,
            List<IAssetName> unloadScenes, string configName = null)
        {
            ref var loadScenes = ref world.NewEntity().Get<ProcessScenesEvent>();
            loadScenes.Name = configName;
            loadScenes.LoadingScenes = configs;
            loadScenes.UnloadingScenes = unloadScenes;
            loadScenes.UnloadOthers = false;
        }

        /// <summary>
        /// Add configuration of the scenes list to load.
        /// LoadScenesComponent will be sent.
        /// </summary>
        /// <param name="world"> Extension to the EcsWorld</param>
        /// <param name="configs">List of the ILoadingSceneConfig to load</param>
        /// <param name="unloadOthers">if true all current non new scenes will be unloaded</param>
        /// <param name="configName">When bunch of the scenes will be loaded, system will be notified with 'ScenesLoadComplete' component and the only field inside is this configName param, so it is possible to identify which set of scenes was loaded.</param>
        public static void LoadScenes(this EcsWorld world, List<ILoadingSceneConfig> configs, bool unloadOthers,
            string configName = null)
        {
            ref var loadScenes = ref world.NewEntity().Get<ProcessScenesEvent>();
            loadScenes.Name = configName;
            loadScenes.LoadingScenes = configs;
            loadScenes.UnloadOthers = unloadOthers;
        }

        /// <summary>
        /// Loads list of scenes by their names and unload list scenes by their names.
        /// </summary>
        public static void LoadScenes(this EcsWorld world, List<IAssetName> loadSceneNames, List<IAssetName> unloadSceneNames,
            string configName = null)
        {
            var configs = new List<ILoadingSceneConfig>();
            
            foreach (var sceneName in loadSceneNames)
            {
                var config = new LoadingSceneConfig {Name = sceneName, IsActive = true};
                configs.Add(config);
            }
            
            ref var loadScenes = ref world.NewEntity().Get<ProcessScenesEvent>();
            loadScenes.Name = configName;
            loadScenes.LoadingScenes = configs;
            loadScenes.UnloadingScenes = unloadSceneNames;
        }

        /// <summary>
        /// Unload scene by the given name.
        /// </summary>
        public static void UnloadScene(this EcsWorld world, IAssetName sceneName, string configName = null)
        {
            var list = new List<IAssetName> {sceneName};
            UnloadScenes(world, list, configName);
        }

        /// <summary>
        /// Unload list of scenes.
        /// Names of scenes should be specified in full name format (path+name).
        /// UnloadScenesComponent will be sent.
        /// </summary>
        public static void UnloadScenes(this EcsWorld world, List<IAssetName> sceneNames, string configName = null)
        {
            ref var processScenesEvent = ref world.NewEntity().Get<ProcessScenesEvent>();
            processScenesEvent.Name = configName;
            processScenesEvent.UnloadingScenes = sceneNames;
        }

        /// <summary>
        /// Unloads all current scenes.
        /// </summary>
        public static void UnloadAllScenes(this EcsWorld world, string configName = null)
        {
            ref var processScenesEvent = ref world.NewEntity().Get<ProcessScenesEvent>();
            processScenesEvent.Name = configName;
            processScenesEvent.UnloadOthers = true;
        }

        /// <summary>
        /// Activate scene by its name.
        /// </summary>
        public static void ActivateScene(this EcsWorld world, IAssetName sceneName, bool isMain = false)
        {
            ref var activateScene = ref world.NewEntity().Get<ActivateSceneEvent>();
            activateScene.SceneName = sceneName;
            activateScene.IsMain = isMain;
        }

        /// <summary>
        /// Deactivate a scene by its name.
        /// </summary>
        public static void DeactivateScene(this EcsWorld world, IAssetName sceneName)
        {
            world.NewEntity().Get<DeactivateSceneEvent>().SceneName = sceneName;
        }
    }
}