using System.Collections.Generic;
using Client.Source.Coins;
using Client.Source.Common.Components;
using Client.Source.Gameplay.Events;
using Client.Source.Hex;
using Leopotam.Ecs;

namespace Client.Source.Gameplay.Systems
{
    public class RemoveClusterSystem : IEcsRunSystem
    {
        private EcsFilter<RemoveClusterEvent> _removeClusterFilter;
        private EcsFilter<LevelConfigComponent> _levelConfigFilter;
        private HexGrid _grid;
        private EcsWorld _world;
        private readonly List<CoinData> _coinDataListHelper = new(10);
        
        public void Run()
        {
            if (_removeClusterFilter.IsEmpty()) return;

            foreach (var idx in _removeClusterFilter)
            {
                ref var removeCluster = ref _removeClusterFilter.Get1(idx);
                _Process(removeCluster.Col, removeCluster.Row, removeCluster.Mask, removeCluster.MinClusterSize);
            }
        }

        private void _Process(int col, int row, int mask, int minClusterSize)
        {
            var isClusterRemoved = _grid.RemoveCluster(col, row, mask, minClusterSize, _coinDataListHelper);

            if (!isClusterRemoved) return;

            foreach (var data in _coinDataListHelper)
                _world.NewEntity().Get<DestroyCoinViewEvent>().CoinView = data.View.gameObject;
            
            _coinDataListHelper.Clear();

            _world.NewEntity().Get<CoinsRemovedEvent>();
        }
    }
}