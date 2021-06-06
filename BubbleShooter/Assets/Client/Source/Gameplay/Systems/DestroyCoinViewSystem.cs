using Client.Source.Gameplay.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Source.Gameplay.Systems
{
    public class DestroyCoinViewSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyCoinViewEvent> _destroyCoinFilter;
        
        public void Run()
        {
            if (_destroyCoinFilter.IsEmpty()) return;

            foreach (var idx in _destroyCoinFilter)
            {
                var coinGO = _destroyCoinFilter.Get1(idx).CoinView;
                Object.Destroy(coinGO);
            }
        }
    }
}