using Client.Source.Gameplay.Components;
using Client.Source.Gameplay.Events;
using Client.Source.Gameplay.Services;
using DG.Tweening;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Source.Gameplay.Systems
{
    public class RefillGunCoinsSystem : IEcsRunSystem
    {
        private EcsFilter<RefillGunCoinsEvent> _refillGunCoinsEventFilter;
        private EcsFilter<GunComponent> _gunFilter;

        private FactoryService _factoryService;
        
        public void Run()
        {
            if (_refillGunCoinsEventFilter.IsEmpty()) return;

            var currentHolder = _gunFilter.Single().Gun.CurrentHolder;
            var nextHolder = _gunFilter.Single().Gun.NextHolder;
            var nextCoin = nextHolder.transform.GetChild(0).gameObject;
            nextCoin.transform.SetParent(currentHolder.transform);
            nextCoin.transform.DOMove(currentHolder.transform.position, 0.1f);
            
            _factoryService.CreateRandomCoin(nextHolder.transform);
        }
    }
}