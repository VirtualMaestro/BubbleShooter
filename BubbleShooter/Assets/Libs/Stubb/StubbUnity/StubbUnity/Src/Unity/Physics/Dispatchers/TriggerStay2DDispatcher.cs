using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerStay2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerStay2D(Collider2D other)
        {
            Dispatcher.World.DispatchTriggerStay2D(Dispatcher, other.GetComponent<IEcsViewLink>(), other);
        }
    }
}