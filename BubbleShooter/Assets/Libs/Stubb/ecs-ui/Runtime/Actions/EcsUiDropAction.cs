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
    /// Ui action for processing OnDrop events.
    /// </summary>
    public sealed class EcsUiDropAction : EcsUiActionBase, IDropHandler {
        void IDropHandler.OnDrop (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiDropEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
                msg.Button = eventData.button;
            }
        }
    }
}