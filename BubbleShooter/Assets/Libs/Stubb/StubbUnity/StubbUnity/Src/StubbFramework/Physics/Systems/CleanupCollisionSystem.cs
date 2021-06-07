using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.Physics.Components;

namespace StubbUnity.StubbFramework.Physics.Systems
{
    public sealed class CleanupCollisionSystem : IEcsRunSystem
    {
        private EcsWorld World;
        private EcsFilter<CleanupCollisionComponent> _cleanupCollisionFilter;
        
        public void Run()
        {
            if (!_cleanupCollisionFilter.IsEmpty())
                _cleanupCollisionFilter.Clear();
            
            World.EndPhysicsFrame();
        }
    }
}