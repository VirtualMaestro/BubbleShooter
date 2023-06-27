using System.Collections.Generic;
using Client.Source.Coins;
using Client.Source.Common.Components;
using Client.Source.Gameplay.Events;
using Client.Source.Hex;
using Leopotam.Ecs;

namespace Client.Source.Gameplay.Systems
{
    public class CoinWildBoostSystem : IEcsRunSystem
    {
        private EcsFilter<CoinComponent, CoinProcessComponent, CoinWildBoosterComponent> _coinWildFilter;
        private EcsFilter<LevelConfigComponent> _levelConfigFilter;

        private HexGrid _grid;
        private EcsWorld _world;

        private readonly List<CoinData> _neighbors = new(10);
        
        public void Run()
        {
            if (_coinWildFilter.IsEmpty()) return;
            var minClusterSize = _levelConfigFilter.Get1(0).LevelConfig.minCoinsToDelete;

            foreach (var idx in _coinWildFilter)
            {
                ref var entity = ref _coinWildFilter.GetEntity(idx);
                entity.Del<CoinProcessComponent>();
            
                var data = _coinWildFilter.Get1(idx).Coin.GetData();
                _grid.GetNeighborsByMask(data, _neighbors);

                foreach (var coinData in _neighbors)
                {
                    ref var removeCluster = ref _world.NewEntity().Get<RemoveClusterEvent>();
                    removeCluster.Col = coinData.Col;
                    removeCluster.Row = coinData.Row;
                    removeCluster.Mask = coinData.Mask;
                    removeCluster.MinClusterSize = minClusterSize;
                }
                
                _neighbors.Clear();
            }
        }
    }
}