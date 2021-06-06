using StubbUnity.Unity.Physics.Settings;
using UnityEngine;

namespace StubbUnity.Unity.Physics
{
    public class CollisionDispatchingSettingsComponent : MonoBehaviour
    {
        [SerializeField] private CollisionDispatchProperties triggerProperties;
        [SerializeField] private CollisionDispatchProperties collisionProperties;
        
        private CollisionDispatchingSettings _collisionDispatchingSettings;
        public CollisionDispatchingSettings DispatchingSettings => _collisionDispatchingSettings;

        private void Awake()
        {
            _collisionDispatchingSettings = new CollisionDispatchingSettings(triggerProperties, collisionProperties, gameObject);
        }

        public void OnDestroy()
        {
            _collisionDispatchingSettings.Dispose();
            _collisionDispatchingSettings = null;
        }
    }
}