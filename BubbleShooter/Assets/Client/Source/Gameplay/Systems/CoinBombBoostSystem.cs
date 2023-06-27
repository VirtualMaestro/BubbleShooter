using System.Collections.Generic;
using Client.Source.Coins;
using Client.Source.Gameplay.Events;
using Client.Source.Hex;
using Leopotam.Ecs;

namespace Client.Source.Gameplay.Systems
{
    public class CoinBombBoostSystem : IEcsRunSystem
    {
        private EcsFilter<CoinComponent, CoinProcessComponent, CoinBombBoosterComponent> _coinBombFilter;
        private HexGrid _grid;
        private EcsWorld _world;
        private readonly List<CoinData> _neighbors = new();
        
        public void Run()
        {
            if (_coinBombFilter.IsEmpty()) return;

            foreach (var idx in _coinBombFilter)
            {
                ref var entity = ref _coinBombFilter.GetEntity(idx);
                entity.Del<CoinProcessComponent>();

                var coinData = entity.Get<CoinComponent>().Coin.GetData();
                var success = _grid.RemoveNeighborsSelf(coinData.Col, coinData.Row, _neighbors);

                if (success)
                {
                    foreach (var data in _neighbors)
                        _world.NewEntity().Get<DestroyCoinViewEvent>().CoinView = data.View.gameObject;
                }
                
                _world.NewEntity().Get<CoinsRemovedEvent>();

                _neighbors.Clear();
            }
        }
    }
}