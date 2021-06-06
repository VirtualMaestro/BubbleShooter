using System.Collections.Generic;
using Client.Source.Gameplay.Components;
using Client.Source.Gameplay.Events;
using Client.Source.Gameplay.Utils;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Source.Gameplay.Systems
{
    public class DrawTrajectorySystem : IEcsRunSystem
    {
        private EcsFilter<StartDrawTrajectoryEvent> _startDrawTrajectoryFilter;
        private EcsFilter<ShootCoinEvent> _stopDrawTrajectoryFilter;
        private EcsFilter<GunComponent> _gunFilter;
        
        private readonly List<Vector3> _path = new List<Vector3>();

        public void Run()
        {
            if (!_stopDrawTrajectoryFilter.IsEmpty())
            {
                _gunFilter.Single().Gun.LineRenderer.enabled = false;
                return;
            }
            
            if (_startDrawTrajectoryFilter.IsEmpty()) return;
            _path.Clear();
            
            ref var trajectoryEvent = ref _startDrawTrajectoryFilter.First().Get<StartDrawTrajectoryEvent>();
            var direction = trajectoryEvent.End - trajectoryEvent.Start;
            var success = TrajectoryUtil.HitTest(trajectoryEvent.Start, direction, _path, out var hitObj, "coin", "ceil");
            var lineRenderer = _gunFilter.Single().Gun.LineRenderer;
            lineRenderer.enabled = success;
            
            if (!success) return;
            
            lineRenderer.positionCount = _path.Count;

            for (var i = 0; i < _path.Count; i++)
            {
                lineRenderer.SetPosition(i, _path[i]);
            }
        }
    }
}