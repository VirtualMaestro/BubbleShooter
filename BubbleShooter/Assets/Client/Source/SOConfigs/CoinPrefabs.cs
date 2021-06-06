using System.Collections.Generic;
using Client.Source.Coins;
using StubbUnity.StubbFramework.Logging;
using UnityEngine;

namespace Client.Source.SOConfigs
{
    [CreateAssetMenu (fileName = "CoinPrefabsSO", menuName = "Game configs/Coin prefabs")]
    public class CoinPrefabs : ScriptableObject
    {
        [SerializeField]
        private GameObject[] coins;
        
        [SerializeField]
        private GameObject[] boostCoins;
        
        private Dictionary<CoinType, GameObject> _coinsMap;

        public GameObject GetCoin(CoinType coinType)
        {
            if (_coinsMap == null)
                _InitCoinsMap();

            if (!_coinsMap.ContainsKey(coinType))
            {
                log.Error("No there");
            }
            
            return _coinsMap[coinType]; 
        }

        public int NumBoosts => boostCoins.Length;

        private void _InitCoinsMap()
        {
            _coinsMap = new Dictionary<CoinType, GameObject>();

            foreach (var coin in coins)
            {
                var type = coin.GetComponent<CoinMonoLink>().GetCoinType();
                _coinsMap[type] = coin;
            }            
            
            foreach (var coin in boostCoins)
            {
                var type = coin.GetComponent<CoinMonoLink>().GetCoinType();
                _coinsMap[type] = coin;
            }
        }
    }
}