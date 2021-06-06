using Client.Source.Common.Events;
using Client.Source.Gameplay.Components;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Source.Common.Systems
{
    public class LoadLoseGameSystem : IEcsRunSystem
    {
        private EcsFilter<GameLoseEvent> _gameLoseFilter;
        private EcsFilter<ShootingTweenComponent> _animationFilter;
        private EcsWorld _world;
        
        public void Run()
        {
            if (!_animationFilter.IsEmpty()) return;
            if (_gameLoseFilter.IsEmpty()) return;
            
            _gameLoseFilter.Clear();
            _world.LoadScene(SceneNames.LoseScreenSceneName, true);
        }
    }
}