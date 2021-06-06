using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Remove.Components;

namespace StubbUnity.StubbFramework.Remove.Systems
{
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
    public sealed class RemoveEntitySystem : IEcsRunSystem
    {
        EcsFilter<RemoveEntityComponent>.Exclude<DelayComponent> _removeFilter;

        public void Run()
        {
            foreach (var index in _removeFilter)
            {
                _removeFilter.GetEntity(index).Destroy();
            }
        }
    }
}