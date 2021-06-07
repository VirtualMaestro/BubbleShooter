using Leopotam.Ecs;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.StubbFramework.Scenes.Events;

namespace StubbUnity.StubbFramework.Scenes.Systems
{
    public sealed class DeactivateSceneSystem : IEcsRunSystem
    {
        private EcsFilter<DeactivateSceneEvent> _deactivateEventFilter;
        private EcsFilter<SceneComponent, IsSceneActiveComponent> _inactiveScenesFilter;

        public void Run()
        {
            if (_deactivateEventFilter.IsEmpty() || _inactiveScenesFilter.IsEmpty()) return;

            foreach (var idx in _deactivateEventFilter)
            {
                var sceneName = _deactivateEventFilter.Get1(idx).SceneName;

                foreach (var idx1 in _inactiveScenesFilter)
                {
                    var scene = _inactiveScenesFilter.Get1(idx1).Scene;

                    if (scene.SceneName.Equals(sceneName))
                        scene.HideContent();
                }
            }
        }
    }
}