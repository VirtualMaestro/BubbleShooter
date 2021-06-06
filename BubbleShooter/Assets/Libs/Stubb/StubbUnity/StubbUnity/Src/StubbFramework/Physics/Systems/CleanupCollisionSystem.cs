using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using StubbUnity.StubbFramework.Physics.Components;

namespace StubbUnity.StubbFramework.Physics.Systems
{
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
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