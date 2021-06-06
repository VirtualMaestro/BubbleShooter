using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerEnter2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            Dispatcher.World.DispatchTriggerEnter2D(Dispatcher, other.GetComponent<IEcsViewLink>(), other);
        }
    }
}