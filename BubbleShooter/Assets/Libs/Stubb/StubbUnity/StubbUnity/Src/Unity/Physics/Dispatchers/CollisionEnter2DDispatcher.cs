using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public class CollisionEnter2DDispatcher : BasePhysicsDispatcher
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            Dispatcher.World.DispatchCollisionEnter2D(Dispatcher, other.gameObject.GetComponent<IEcsViewLink>(), other);
        }
    }
}