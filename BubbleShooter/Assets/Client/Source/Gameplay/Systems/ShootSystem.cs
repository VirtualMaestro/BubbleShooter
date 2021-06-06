using System.Collections.Generic;
using Client.Source.Coins;
using Client.Source.Common.Components;
using Client.Source.Common.Events;
using Client.Source.Gameplay.Components;
using Client.Source.Gameplay.Events;
using Client.Source.Gameplay.Extensions;
using Client.Source.Gameplay.Utils;
using Client.Source.Hex;
using DG.Tweening;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Source.Gameplay.Systems
{
    public class ShootSystem : IEcsRunSystem
    {
        private EcsFilter<ShootCoinEvent> _shootEventFilter;
        private EcsFilter<GunComponent> _gunFilter;
        private EcsFilter<CoinsHolderComponent> _coinsHolderFilter;
        private EcsFilter<LevelConfigComponent> _currentLevelConfigFilter;

        private EcsWorld _world;
        private HexGrid _hexGrid;
        private readonly List<Vector3> _path = new List<Vector3>();

        public void Run()
        {
            if (_shootEventFilter.IsEmpty()) return;
            _path.Clear();
            
            ref var shootEvent = ref _shootEventFilter.First().Get<ShootCoinEvent>();
            var direction = shootEvent.End - shootEvent.Start;
            var success = TrajectoryUtil.HitTest(shootEvent.Start, direction, _path, out var hitObj, "coin", "ceil");

            if (!success) return;
            
            var currentHolder = _gunFilter.Single().Gun.CurrentHolder;
            var currentCoin = currentHolder.transform.GetChild(0).gameObject;
            var currentCoinData = currentCoin.GetComponent<CoinMonoLink>().GetData();
            var coinsHolder = _coinsHolderFilter.Single().CoinsHolder.gameObject;
            var hitPoint = hitObj.point;
            var coinSize = _currentLevelConfigFilter.Single().LevelConfig.coinSize;
            var hitObjGO = hitObj.collider.gameObject;

            for (var i = 0; i < _path.Count; i++)
            {
                var vector3 = _path[i];
                _path[i] = coinsHolder.transform.InverseTransformPoint(vector3);
            }

            if (hitObj.collider.CompareTag("coin"))
            {
                var hitCoinData = hitObjGO.GetComponent<CoinMonoLink>().GetData();
                var hitCoinPosition = hitObjGO.transform.position;
                var neighborOffset = HexGrid.GetNeighborOffset(hitCoinPosition, hitPoint);
                var isSuccess = _hexGrid.GetFreePositionByNeighbor(hitCoinData.Col, hitCoinData.Row, neighborOffset, out var resultPosition);

                if (isSuccess)
                {
                    currentCoin.transform.SetParent(coinsHolder.transform);
                    currentCoinData.SetPosition(resultPosition.x, resultPosition.y);
                }
                else // maybe reach the bottom and game over at the corner
                {
                    var failPosition = _hexGrid.GetPositionByNeighbor(hitCoinData.Col, hitCoinData.Row, neighborOffset);

                    if (failPosition.y >= _hexGrid.Rows)
                    {
                        _world.NewEntity().Get<GameLoseEvent>();
                    }
                    
                    return;
                }
            } 
            else if (hitObj.collider.CompareTag("ceil"))
            {
                var localPoint = coinsHolder.transform.InverseTransformPoint(hitPoint);
                var col = (int) (localPoint.x / coinSize);
                var row = (int) (localPoint.y / coinSize);

                if (!_hexGrid.IsValid(col, row) || !_hexGrid.IsFree(col, row)) return;
                
                currentCoin.transform.SetParent(coinsHolder.transform);
                currentCoinData.SetPosition(col, row);
            }
            
            _hexGrid.Add(currentCoinData.Col, currentCoinData.Row, currentCoinData);

            var newLocalPoint = coinsHolder.transform.GetLocalFromGrid(currentCoinData.Col, currentCoinData.Row, coinSize);
            _path[_path.Count - 1] = newLocalPoint;

            var animationId = currentCoin.transform.DOLocalPath(_path.ToArray(), 0.5f).intId;
            var coinMonoLink = currentCoin.GetComponent<CoinMonoLink>();
            coinMonoLink.GetEntity().Get<ShootingTweenComponent>().TweenId = animationId;
            coinMonoLink.TrailEnable(true);
            
            
            _world.NewEntity().Get<RefillGunCoinsEvent>();
            _world.NewEntity().Get<BlockInputComponent>();
        }
    }
}