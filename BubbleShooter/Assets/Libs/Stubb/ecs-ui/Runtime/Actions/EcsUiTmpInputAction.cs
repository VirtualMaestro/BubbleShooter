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
    [RequireComponent (typeof (TMP_InputField))]
    public sealed class EcsUiTmpInputAction : EcsUiActionBase {
        TMP_InputField _input;

        void Awake () {
            _input = GetComponent<TMP_InputField> ();
            _input.onValueChanged.AddListener (OnInputValueChanged);
            _input.onEndEdit.AddListener (OnInputEnded);
        }

        void OnInputValueChanged (string value) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiTmpInputChangeEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = _input;
                msg.Value = value;
            }
        }

        void OnInputEnded (string value) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Get<EcsUiTmpInputEndEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = _input;
                msg.Value = value;
            }
        }
    }
}