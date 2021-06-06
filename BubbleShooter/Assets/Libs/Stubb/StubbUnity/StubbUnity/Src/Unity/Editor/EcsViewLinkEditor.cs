using StubbUnity.Unity.Extensions;
using StubbUnity.Unity.Physics;
using StubbUnity.Unity.View;
using UnityEditor;

namespace StubbUnity.Unity.Editor
{
    [CustomEditor(typeof(EcsViewLink))]
    public class EcsViewLinkEditor : UnityEditor.Editor
    {
        private bool _hasPhysics;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var viewLink = (EcsViewLink) target;

            if (viewLink.hasPhysics == _hasPhysics) return;
            
            _hasPhysics = viewLink.hasPhysics;
            viewLink.gameObject.EnableComponent<CollisionDispatchingSettingsComponent>(_hasPhysics);
        }
    }
}