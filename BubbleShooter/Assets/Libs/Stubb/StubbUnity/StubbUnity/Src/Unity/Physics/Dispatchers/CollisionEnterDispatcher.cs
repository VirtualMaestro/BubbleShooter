using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionEnterDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionEnter(Collision other)
        {
            Dispatcher.World.DispatchCollisionEnter(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}