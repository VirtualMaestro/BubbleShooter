using Leopotam.Ecs;
using StubbUnity.StubbFramework.Pause.Components;
using StubbUnity.StubbFramework.Pause.Events;
using StubbUnity.StubbFramework.View.Components;

namespace StubbUnity.StubbFramework.Pause.Systems
{
    public class PauseGameSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PauseGameEvent> _pauseGameFilter = null;
        private readonly EcsFilter<EcsViewLinkComponent> _viewLinkFilter = null;
        
        public void Run()
        {
            if (_pauseGameFilter.IsEmpty()) return;

            foreach (var idx in _viewLinkFilter)
                _viewLinkFilter.Get1(idx).Value.OnPause();

            _world.NewEntity().Get<GameOnPauseComponent>();
        }
    }
}