using Client.Source.Common.Events;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Source.Common.Systems
{
    public class LoadWinGameSystem : IEcsRunSystem
    {
        private EcsFilter<GameWinEvent> _gameWinFilter;
        private EcsWorld _world;

        public void Run()
        {
            if (_gameWinFilter.IsEmpty()) return;

            _world.LoadScene(SceneNames.WinScreenSceneName, true);
        }
    }
}