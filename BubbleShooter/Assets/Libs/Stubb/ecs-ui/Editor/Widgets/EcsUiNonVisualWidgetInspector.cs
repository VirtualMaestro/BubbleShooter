// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEditor.UI;

namespace Leopotam.Ecs.Ui.Widgets.UnityEditors {
    [CustomEditor (typeof (EcsUiNonVisualWidget), false)]
    [CanEditMultipleObjects]
    sealed class EcsUiNonVisualWidgetInspector : GraphicEditor {
        public override void OnInspectorGUI () {
            serializedObject.Update ();
            EditorGUILayout.PropertyField (m_Script);
            RaycastControlsGUI ();
            serializedObject.ApplyModifiedProperties ();
        }
    }
}