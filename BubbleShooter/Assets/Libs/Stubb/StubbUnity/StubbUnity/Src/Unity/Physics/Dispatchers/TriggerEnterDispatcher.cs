using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerEnterDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerEnter(Collider other)
        {
            Dispatcher.World.DispatchTriggerEnter(Dispatcher, other.GetComponent<IEcsViewLink>(), other);
        }
    }
}