using StubbUnity.Unity.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics
{
    public class BasePhysicsDispatcher : MonoBehaviour
    {
        protected EcsViewLink Dispatcher;
        
        private void Start()
        {
            Dispatcher = GetComponent<EcsViewLink>();
        }
    }
}