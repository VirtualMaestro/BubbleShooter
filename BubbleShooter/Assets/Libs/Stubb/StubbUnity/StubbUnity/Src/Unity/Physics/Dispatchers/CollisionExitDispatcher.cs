using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionExitDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionExit(Collision other)
        {
            Dispatcher.World.DispatchCollisionExit(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}