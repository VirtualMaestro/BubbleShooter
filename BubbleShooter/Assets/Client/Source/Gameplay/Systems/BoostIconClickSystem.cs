using Client.Source.Coins;
using Client.Source.Gameplay.Components;
using Client.Source.Gameplay.Events;
using DG.Tweening;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Source.Gameplay.Systems
{
    public class BoostIconClickSystem : IEcsRunSystem
    {
        private EcsFilter<BoostIconClickEvent> _boostIconClickFilter;
        private EcsFilter<GunComponent> _gunFilter;
        private EcsFilter<BoostIconComponent> _boostIconsFilter;
        private EcsWorld _world;
        
        public void Run()
        {
            if (_boostIconClickFilter.IsEmpty()) return;

            var coinGO = _boostIconClickFilter.Get1(0).Coin;
            var gunCurrentHolder = _gunFilter.Single().Gun.CurrentHolder;
            var coinData = gunCurrentHolder.transform.GetChild(0).GetComponent<CoinMonoLink>().GetData();
            
            _world.NewEntity().Get<DestroyCoinViewEvent>().CoinView = coinData.View.gameObject;
            
            coinGO.transform.SetParent(gunCurrentHolder.transform);
            coinGO.transform.DOMove(gunCurrentHolder.transform.position, 0.5f);
            
            //
            foreach (var idx in _boostIconsFilter)
            {
                var boostIcon = _boostIconsFilter.Get1(idx).BoostIcon;
                if (boostIcon.IsEmpty)
                    _boostIconsFilter.GetEntity(idx).DestroyDelayEndFrame();
            }
        }
    }
}