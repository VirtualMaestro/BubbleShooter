using Client.Source.Coins;
using Client.Source.Gameplay.Components;
using DG.Tweening;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Source.Gameplay.Systems
{
    public class ShootingTweenSystems : IEcsRunSystem
    {
        private EcsFilter<CoinComponent, ShootingTweenComponent> _animationFilter;
        private EcsFilter<BlockInputComponent> _blockInputFilter;
        private EcsWorld _world;
        
        public void Run()
        {
            if (_animationFilter.IsEmpty()) return;

            ref var entity = ref _animationFilter.GetEntity(0);

            var tweenId = entity.Get<ShootingTweenComponent>().TweenId;
            
            if (DOTween.IsTweening(tweenId) == false)
            {
                entity.Del<ShootingTweenComponent>();
                entity.Get<CoinProcessComponent>();

                entity.Get<CoinComponent>().Coin.TrailEnable(false);
                
                _blockInputFilter.Clear();
            }
        }
    }
}