// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using TMPro;
using UnityEngine;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing InputField events.
    /// </summary>
    [RequireComponent (typeof (TMP_Dropdown))]
    public sealed class EcsUiTmpDropdownAction : EcsUiActionBase {
        TMP_Dropdown _dropdown;

        void Awake () {
            _dropdown = GetComponent<TMP_Dropdown> ();
            _dropdown.onValueChanged.AddListener (OnDropdownValueChanged);
        }

        void OnDropdownValueChanged (int value) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiTmpDropdownChangeEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = _dropdown;
                msg.Value = value;
            }
        }
    }
}