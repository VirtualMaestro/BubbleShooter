using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.Pause.Components;
using StubbUnity.StubbFramework.Pause.Events;
using StubbUnity.StubbFramework.View.Components;

namespace StubbUnity.StubbFramework.Pause.Systems
{
    public class ResumeGameSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameOnPauseComponent> _gameOnPauseFilter = null;
        private readonly EcsFilter<ResumeGameEvent> _resumeGameFilter = null;
        private readonly EcsFilter<EcsViewLinkComponent> _viewLinkFilter = null;

        public void Run()
        {
            if (_resumeGameFilter.IsEmpty()) return;
            
            foreach (var idx in _viewLinkFilter)
                _viewLinkFilter.Get1(idx).Value.OnResume();
            
            _gameOnPauseFilter.Clear();
        }
    }
}