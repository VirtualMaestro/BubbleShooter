using Client.Source.Coins;
using Client.Source.Common.Components;
using Client.Source.Gameplay.Events;
using Leopotam.Ecs;

namespace Client.Source.Gameplay.Systems
{
    public class CoinProcessSystem : IEcsRunSystem
    {
        private EcsFilter<CoinComponent, CoinProcessComponent> _coinProcessFilter;
        private EcsFilter<LevelConfigComponent> _levelFilter;
        private EcsWorld _world;
        
        public void Run()
        {
            if (_coinProcessFilter.IsEmpty()) return;

            var minCluster = _levelFilter.Get1(0).LevelConfig.minCoinsToDelete;
            
            foreach (var idx in _coinProcessFilter)
            {
                ref var entity = ref _coinProcessFilter.GetEntity(idx);
                entity.Del<CoinProcessComponent>();

                var data = entity.Get<CoinComponent>().Coin.GetData();
                ref var removeCluster = ref _world.NewEntity().Get<RemoveClusterEvent>();
                removeCluster.Col = data.Col;
                removeCluster.Row = data.Row;
                removeCluster.Mask = data.Mask;
                removeCluster.MinClusterSize = minCluster;
            }
        }
    }
}