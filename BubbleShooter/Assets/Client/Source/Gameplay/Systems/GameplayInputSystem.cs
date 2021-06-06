using System.Runtime.CompilerServices;
using Client.Source.Common;
using Client.Source.Common.Components;
using Client.Source.Gameplay.Components;
using Client.Source.Gameplay.Events;
using Client.Source.Gameplay.Mono;
using Client.Source.SOConfigs;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View.Components;
using UnityEngine;

namespace Client.Source.Gameplay.Systems
{
    public class GameplayInputSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private readonly EcsFilter<EcsUiDownEvent> _downEvents = null;
        private readonly EcsFilter<EcsUiUpEvent> _upEvents = null;
        private readonly EcsFilter<EcsUiDragEvent> _dragEvents = null;
        private readonly EcsFilter<GunComponent> _gunFilter = null;
        private readonly EcsFilter<BlockInputComponent> _blockInputFilter = null;
        private readonly EcsFilter<CameraComponent> _cameraFilter = null;
        private readonly AppSettings _settings;

        public void Run()
        {
            if (!_blockInputFilter.IsEmpty()) return;
            
            if (!_upEvents.IsEmpty())
            {
                ref var stopDrawTrajectoryEvent = ref _world.NewEntity().Get<ShootCoinEvent>();
                stopDrawTrajectoryEvent.Start = _GetStartPosition();
                stopDrawTrajectoryEvent.End = _GetEndPosition(_upEvents.First().Get<EcsUiUpEvent>().Position);
                return;
            }
            
            var isDragEmpty = _dragEvents.IsEmpty();
            var isDownEmpty = _downEvents.IsEmpty();

            if (isDragEmpty && isDownEmpty) return;

            Vector2 position;
            if (!isDragEmpty)
            {
                ref var dragEvent = ref _dragEvents.First().Get<EcsUiDragEvent>();
                if (dragEvent.WidgetName != WidgetNames.GamePlayDrag) return;

                position = dragEvent.Position;
            }
            else
            {
                ref var downEvent = ref _downEvents.First().Get<EcsUiDownEvent>();
                if (downEvent.WidgetName != WidgetNames.GamePlayDownUp) return;

                position = downEvent.Position;
            }

            ref var startDrawTrajectoryEvent = ref _world.NewEntity().Get<StartDrawTrajectoryEvent>();
            startDrawTrajectoryEvent.Start = _GetStartPosition();
            startDrawTrajectoryEvent.End = _GetEndPosition(position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 _GetStartPosition()
        {
            var gun = _gunFilter.First().Get<EcsViewLinkComponent>().Value as GunMonoLink;
            return gun.CurrentHolder.transform.position + Vector3.up;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 _GetEndPosition(Vector3 screenPosition)
        {
            var camera = _cameraFilter.Get1(0).Camera;
            var endWorld = camera.ScreenToWorldPoint(screenPosition);
            endWorld.z = 0;
            return endWorld;
        }
    }
}