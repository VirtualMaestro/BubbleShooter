using System.Collections.Generic;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Pooling;

namespace StubbUnity.StubbFramework.Scenes.Configurations
{
    public class ProcessSetScenesConfig
    {
        public string Name;
        public bool UnloadOthers;

        public List<ILoadingSceneConfig> LoadingList { get; }
        public List<IAssetName> UnloadingList { get; }

        public bool IsEmpty => LoadingList.Count == 0;
        public bool HasUnloading => UnloadOthers || UnloadingList.Count > 0;
        
        public ProcessSetScenesConfig()
        {
            LoadingList = new List<ILoadingSceneConfig>();
            UnloadingList = new List<IAssetName>();
        }
        
        public void AddToLoad(ILoadingSceneConfig config)
        {
            LoadingList.Add(config);
        }

        public void AddToLoad(List<ILoadingSceneConfig> configs)
        {
            if (configs == null) return;
            
            foreach (var config in configs)
            {
                LoadingList.Add(config);
            }
        }

        public void AddToUnload(IAssetName sceneName)
        {
            UnloadingList.Add(sceneName);
        }
        
        public void AddToUnload(List<IAssetName> sceneNames)
        {
            if (sceneNames == null) return;
            
            foreach (var sceneName in sceneNames)
            {
                UnloadingList.Add(sceneName);
            }
        }
        
        public ILoadingSceneConfig RemoveFromLoadingList(string sceneName)
        {
            ILoadingSceneConfig config = null;

            for (var i = 0; i < LoadingList.Count; i++)
            {
                var cfg = LoadingList[i];

                if (cfg.Name.FullName.Equals(sceneName))
                {
                    config = cfg;
                    LoadingList.RemoveAt(i);

                    break;
                }
            }

            return config;
        }

        public void Dispose()
        {
            LoadingList.Clear();
            UnloadingList.Clear();
            
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