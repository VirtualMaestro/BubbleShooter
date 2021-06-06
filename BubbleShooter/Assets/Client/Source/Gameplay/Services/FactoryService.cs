using System;
using Client.Source.Coins;
using Client.Source.SOConfigs;
using StubbUnity.StubbFramework.Logging;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Client.Source.Gameplay.Services
{
    public class FactoryService
    {
        private readonly AppSettings _settings;
        private readonly Array _enums;
        
        public FactoryService(AppSettings settings)
        {
            _settings = settings;
            _enums = Enum.GetValues(typeof(CoinType)); 
        }

        public GameObject CreateCoin(CoinType coinType, Transform parent)
        {
            var prefab = _settings.coins.GetCoin(coinType);
            var coin = Object.Instantiate(prefab, parent);
            coin.GetComponent<CoinMonoLink>().SetData(CoinData.Get(coinType));
            return coin;
        }

        public GameObject CreateCoin(CoinData data, Transform parent)
        {
            var prefab = _settings.coins.GetCoin(data.CoinType);
            var coin = Object.Instantiate(prefab, parent);
            coin.GetComponent<CoinMonoLink>().SetData(data);
            return coin;
        }

        public GameObject CreateRandomCoin(Transform parent)
        {
            var coinType = GetRandomCoinType();
            return CreateCoin(coinType, parent);
        }

        public CoinType GetRandomCoinType()
        {
            var randIndex = Random.Range(0, _enums.Length - _settings.coins.NumBoosts);
            var coinType = (CoinType) _enums.GetValue(randIndex);
            return coinType;
        }
            

        public CoinData CreateCoinData()
        {
            var t = GetRandomCoinType();
            log.Info($"Random coin type: {t}");
            return CoinData.Get(t);
        }
        
        public CoinData[] CreateCoinsData(int numCoins)
        {
            var coins = new CoinData[numCoins];
           
            for (var i = 0; i < numCoins; i++)
            {
                var coinData = CreateCoinData();
                coins[i] = coinData;
            }

            return coins;
        }
        
        public GameObject CreateBoostIcon(GameObject prefab, Transform parent)
        {
            return Object.Instantiate(prefab, parent);
        }
    }
}