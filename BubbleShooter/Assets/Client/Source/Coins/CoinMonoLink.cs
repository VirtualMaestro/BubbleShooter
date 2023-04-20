using Leopotam.Ecs;
using StubbUnity.Unity.View;
using UnityEngine;

namespace Client.Source.Coins
{
    public class CoinMonoLink : EcsViewLink
    {
        [SerializeField] private CoinType type;
        [SerializeField] private TrailRenderer trail;

        private CoinData _data;

        public CoinType GetCoinType() => _data?.CoinType ?? type;
        public CoinData GetData() => _data;
        public void TrailEnable(bool value)
        {
            if (trail == null) return;
            trail.enabled = value;
        }
        
        public void SetData(CoinData coinData)
        {
            _data = coinData;
            _data.View = this;
        }

        public override void OnInitialize()
        {
            GetEntity().Get<CoinComponent>().Coin = this;

            if (GetCoinType() == CoinType.Coin_Wild)
                GetEntity().Get<CoinWildBoosterComponent>();
            else if (GetCoinType() == CoinType.Coin_Bomb)
                GetEntity().Get<CoinBombBoosterComponent>();
        }
    }
}