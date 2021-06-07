using Leopotam.Ecs;
using StubbUnity.StubbFramework.Scenes.Configurations;
using StubbUnity.StubbFramework.Scenes.Events;
using StubbUnity.Unity.Scenes;

namespace StubbUnity.StubbFramework.Scenes.Systems
{
    public sealed class ProcessScenesSystem : IEcsRunSystem
    {
        private EcsFilter<ProcessScenesEvent> _loadScenesFilter;
        private SceneService _sceneService;

        public void Run()
        {
            if (_loadScenesFilter.IsEmpty()) return;

            foreach (var idx in _loadScenesFilter)
            {
                ref var loadScenes = ref _loadScenesFilter.Get1(idx);

                _sceneService.Process(_CreateSeProcessConfig(loadScenes));
            }
        }

        private ProcessSetScenesConfig _CreateSeProcessConfig(ProcessScenesEvent processSceneEvent)
        {
            var loadingSceneSet = ProcessSetScenesConfig.Get();
            loadingSceneSet.Name = processSceneEvent.Name;
            loadingSceneSet.UnloadOthers = processSceneEvent.UnloadOthers;
            loadingSceneSet.AddToLoad(processSceneEvent.LoadingScenes);
            loadingSceneSet.AddToUnload(processSceneEvent.UnloadingScenes);

            return loadingSceneSet;
        }
    }
}