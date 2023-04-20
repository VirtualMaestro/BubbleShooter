using StubbUnity.Unity.Physics.Settings;
using UnityEngine;

namespace StubbUnity.Unity.Physics
{
    public class CollisionDispatchingSettingsComponent : MonoBehaviour
    {
        [SerializeField] private CollisionDispatchProperties triggerProperties;
        [SerializeField] private CollisionDispatchProperties collisionProperties;

        public CollisionDispatchingSettings DispatchingSettings { get; private set; }

        private void Awake()
        {
            DispatchingSettings = new CollisionDispatchingSettings(triggerProperties, collisionProperties, gameObject);
        }

        public void OnDestroy()
        {
            DispatchingSettings.Dispose();
            DispatchingSettings = null;
        }
    }
}