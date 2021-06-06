using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionExit2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionExit2D(Collision2D other)
        {
            Dispatcher.World.DispatchCollisionExit2D(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}