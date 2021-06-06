using System.Collections.Generic;
using Client.Source.Coins;
using Client.Source.Gameplay.Events;
using Client.Source.Hex;
using Leopotam.Ecs;

namespace Client.Source.Gameplay.Systems
{
    public class RemoveDisconnectedSystem : IEcsRunSystem
    {
        private EcsFilter<CoinsRemovedEvent> _coinsRemovedFilter;
        private HexGrid _grid;
        private EcsWorld _world;
        private readonly List<CoinData> _coinDataListHelper = new List<CoinData>(10);

        public void Run()
        {
            if (_coinsRemovedFilter.IsEmpty()) return;
            
            _grid.RemoveDisconnected(_coinDataListHelper);
            
            foreach (var data in _coinDataListHelper)
                _world.NewEntity().Get<DestroyCoinViewEvent>().CoinView = data.View.gameObject;
            
            _coinDataListHelper.Clear();
        }
    }
}