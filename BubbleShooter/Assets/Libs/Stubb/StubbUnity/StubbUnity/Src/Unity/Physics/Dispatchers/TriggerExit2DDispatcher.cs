using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.View;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Dispatchers
{
    public sealed class TriggerExit2DDispatcher : BasePhysicsDispatcher
    {
        void OnTriggerExit2D(Collider2D other)
        {
            Dispatcher.World.DispatchTriggerExit2D(Dispatcher, other.GetComponent<IEcsViewLink>(), other);
        }
    }
}