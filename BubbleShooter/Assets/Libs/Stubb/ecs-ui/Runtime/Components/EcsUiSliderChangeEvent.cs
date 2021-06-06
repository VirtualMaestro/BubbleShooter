// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine.UI;

namespace Leopotam.Ecs.Ui.Components {
    public struct EcsUiSliderChangeEvent {
        public string WidgetName;
        public Slider Sender;
        public float Value;
    }
}