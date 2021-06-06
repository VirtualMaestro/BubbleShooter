using Leopotam.Ecs.Ui.Systems;
using StubbUnity.Unity.Extensions;
using UnityEditor;

namespace StubbUnity.Unity.Editor
{
    [CustomEditor(typeof(EntryPoint), true)]
    public class UIEnablerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var view = (EntryPoint) target;
            var go = view.gameObject;
            
            if (view.enableUiEmitter)
            {
                if (!go.HasComponent<EcsUiEmitter>())
                    go.AddComponent<EcsUiEmitter>();
            }
            else if (go.HasComponent<EcsUiEmitter>())
                DestroyImmediate(go.GetComponent<EcsUiEmitter>());
        }
    }
}