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
    /// Ui action for processing down / up input events.
    /// </summary>
    public sealed class EcsUiDownUpAction : EcsUiActionBase, IPointerDownHandler, IPointerUpHandler {
        void IPointerDownHandler.OnPointerDown (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiDownEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
                msg.Position = eventData.position;
                msg.PointerId = eventData.pointerId;
                msg.Button = eventData.button;
            }
        }

        void IPointerUpHandler.OnPointerUp (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiUpEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
                msg.Position = eventData.position;
                msg.PointerId = eventData.pointerId;
                msg.Button = eventData.button;
            }
        }
    }
}