using System.Collections.Generic;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Pooling;

namespace StubbUnity.StubbFramework.Scenes.Configurations
{
    public class ProcessSetScenesConfig
    {
        private List<ILoadingSceneConfig> _loadingList;
        private List<IAssetName> _unloadingList;

        public string Name;
        public bool UnloadOthers;

        public List<ILoadingSceneConfig> LoadingList => _loadingList;
        public List<IAssetName> UnloadingList => _unloadingList;
        public bool IsEmpty => _loadingList.Count == 0;
        public bool HasUnloading => UnloadOthers || _unloadingList.Count > 0;
        
        public ProcessSetScenesConfig()
        {
            _loadingList = new List<ILoadingSceneConfig>();
            _unloadingList = new List<IAssetName>();
        }
        
        public void AddToLoad(ILoadingSceneConfig config)
        {
            _loadingList.Add(config);
        }

        public void AddToLoad(List<ILoadingSceneConfig> configs)
        {
            if (configs == null) return;
            
            foreach (var config in configs)
            {
                _loadingList.Add(config);
            }
        }

        public void AddToUnload(IAssetName sceneName)
        {
            _unloadingList.Add(sceneName);
        }
        
        public void AddToUnload(List<IAssetName> sceneNames)
        {
            if (sceneNames == null) return;
            
            foreach (var sceneName in sceneNames)
            {
                _unloadingList.Add(sceneName);
            }
        }
        
        public ILoadingSceneConfig RemoveFromLoadingList(string sceneName)
        {
            ILoadingSceneConfig config = null;

            for (var i = 0; i < _loadingList.Count; i++)
            {
                var cfg = _loadingList[i];

                if (cfg.Name.FullName.Equals(sceneName))
                {
                    config = cfg;
                    _loadingList.RemoveAt(i);

                    break;
                }
            }

            return config;
        }

        public void Dispose()
        {
            _loadingList.Clear();
            _unloadingList.Clear();
            
            UnloadOthers = false;
            Name = null;
            
            Pools.I.Get<ProcessSetScenesConfig>().Put(this);
        }

        //
        public static ProcessSetScenesConfig Get()
        {
            return Pools.I.Get<ProcessSetScenesConfig>().Get();
        }
    }
}