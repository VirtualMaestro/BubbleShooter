// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;

namespace Leopotam.Ecs.Ui.Components {
    public struct EcsUiDropEvent {
        public string WidgetName;
        public GameObject Sender;
        public PointerEventData.InputButton Button;
    }
}