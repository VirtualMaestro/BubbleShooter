using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionStayDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionStay(Collision other)
        {
            Dispatcher.World.DispatchCollisionStay(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}