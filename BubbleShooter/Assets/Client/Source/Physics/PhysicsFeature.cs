using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Source.Physics
{
    public class PhysicsFeature : EcsFeature, IEcsInitSystem
    {
        public PhysicsFeature()
        {
            // Add collision handling systems
        }

        public void Init()
        {
            // Setup collision pair. E.g.: World.AddCollisionPair(1, 2); 
        }
    }
}