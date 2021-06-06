// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2021 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.Ecs.Ui.Widgets {
    /// <summary>
    /// Non visual interactive Ui widget, keep fillrate / no rendering / alpha-blending.
    /// </summary>
    [RequireComponent (typeof (CanvasRenderer))]
    [RequireComponent (typeof (RectTransform))]
    public class EcsUiNonVisualWidget : Graphic {
        public override void SetMaterialDirty () { }
        public override void SetVerticesDirty () { }
        public override Material material { get { return defaultMaterial; } set { } }
        public override void Rebuild (CanvasUpdate update) { }
    }
}