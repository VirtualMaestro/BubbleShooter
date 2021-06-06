using Client.Source.Coins;
using Client.Source.Common.Components;
using Client.Source.Common.Events;
using Client.Source.Gameplay.Components;
using Client.Source.Gameplay.Extensions;
using Client.Source.Gameplay.Mono;
using Client.Source.Gameplay.Services;
using Client.Source.Hex;
using Client.Source.SOConfigs;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Source.Gameplay.Systems
{
    public class BuildLevelSystem : IEcsRunSystem
    {
        private EcsFilter<BuildLevelEvent> _buildLevelFilter;
        private EcsFilter<LevelConfigComponent> _currentLevelConfigFilter;
        private EcsFilter<CoinsHolderComponent> _coinsHolderFilter;
        private EcsFilter<BoostIconsHolderComponent> _boostIconsHolderFilter;
        private EcsFilter<GunComponent> _gunFilter;
        
        private HexGrid _hexGrid;
        private FactoryService _factoryService;
        
        private LevelConfig _levelConfig;

        public void Run()
        {
            if (_buildLevelFilter.IsEmpty()) return;

            _levelConfig = _currentLevelConfigFilter.Single().LevelConfig;
            
            _hexGrid.Init(_levelConfig.boardColumns, _levelConfig.boardRows);
            _hexGrid.Populate(_factoryService.CreateCoinsData(_levelConfig.numCoins));
            _hexGrid.ForEach(_CreateVisual);

            _InitGun();
            _InitBoosts();
        }

        private void _CreateVisual(int col, int row, CoinData data)
        {
            if (data == null) return;
            
            var coinsHolder = _coinsHolderFilter.Single().CoinsHolder.gameObject;
            var coin = _factoryService.CreateCoin(data, coinsHolder.transform);
            coin.transform.SetLocalFromGrid(col, row, _levelConfig.coinSize);
        }

        private void _InitGun()
        {
            var currentHolder = _gunFilter.Single().Gun.CurrentHolder;
            var nextHolder = _gunFilter.Single().Gun.NextHolder;
            
            _factoryService.CreateRandomCoin(currentHolder.transform);
            _factoryService.CreateRandomCoin(nextHolder.transform);
        }

        private void _InitBoosts()
        {
            if (_boostIconsHolderFilter.IsEmpty()) return;

            var iconsHolder = _boostIconsHolderFilter.Single().BoostIconsHolder;
            var startVerticalPos = 0f;
            var gap = 0.2f;
            
            foreach (var coinType in _levelConfig.GetBoostTypes)
            {
                var boostIcon = _factoryService.CreateBoostIcon(_levelConfig.boostIconPrefab, iconsHolder.transform);
                var iconHolder = boostIcon.GetComponent<BoostIconMonoLink>().BoostIconHolder;
                _factoryService.CreateCoin(coinType, iconHolder.transform);
                
                boostIcon.transform.localPosition += new Vector3(0, -startVerticalPos);
                
                var size = boostIcon.GetComponent<BoxCollider2D>().bounds.size;
                startVerticalPos += size.y + gap;
            }
        }
    }
}