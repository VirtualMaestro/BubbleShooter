// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing OnBeginDrag / OnDrag / OnEndDrag events.
    /// </summary>
    public sealed class EcsUiDragAction : EcsUiActionBase, IBeginDragHandler, IDragHandler, IEndDragHandler {
        void IBeginDragHandler.OnBeginDrag (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiBeginDragEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
                msg.Position = eventData.position;
                msg.PointerId = eventData.pointerId;
                msg.Button = eventData.button;
            }
        }

        void IDragHandler.OnDrag (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiDragEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
                msg.Position = eventData.position;
                msg.PointerId = eventData.pointerId;
                msg.Delta = eventData.delta;
                msg.Button = eventData.button;
            }
        }

        void IEndDragHandler.OnEndDrag (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiEndDragEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
                msg.Position = eventData.position;
                msg.PointerId = eventData.pointerId;
                msg.Button = eventData.button;
            }
        }
    }
}