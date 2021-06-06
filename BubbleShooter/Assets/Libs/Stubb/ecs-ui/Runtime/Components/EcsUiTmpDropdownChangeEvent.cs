// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using TMPro;

namespace Leopotam.Ecs.Ui.Components {
    public struct EcsUiTmpDropdownChangeEvent {
        public string WidgetName;
        public TMP_Dropdown Sender;
        public int Value;
    }
}