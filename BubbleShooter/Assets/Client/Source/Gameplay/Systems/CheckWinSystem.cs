using Client.Source.Common.Events;
using Client.Source.Gameplay.Events;
using Client.Source.Hex;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Source.Gameplay.Systems
{
    public class CheckWinSystem : IEcsRunSystem
    {
        private EcsFilter<CoinsRemovedEvent> _coinsRemovedFilter;
        
        private HexGrid _grid;
        private EcsWorld _world;
        
        public void Run()
        {
            if (_coinsRemovedFilter.IsEmpty()) return;

            if (_grid.Count == 0)
            {
                _world.NewEntity().Get<GameWinEvent>();
                _coinsRemovedFilter.Clear();
            }
        }
    }
}