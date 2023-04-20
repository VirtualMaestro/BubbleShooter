using StubbUnity.Unity.Pooling;
using StubbUnity.Unity.Scenes;
using UnityEditor;

namespace StubbUnity.Unity.Editor
{
    [CustomEditor(typeof(SceneController), true)]
    public class SceneControllerEditor : UnityEditor.Editor
    {
        private SceneController _controller;
        private bool _hasPooling;
        
        private void OnEnable()
        {
            _controller = (SceneController)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (_hasPooling == _controller.hasPooling) 
                return;
            
            _hasPooling = _controller.hasPooling;
                
            if (_hasPooling)
                _controller.gameObject.AddComponent<PoolsController>();
            else
                DestroyImmediate(_controller.gameObject.GetComponent<PoolsController>());
        }
    }
}