using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Remove.Components;

namespace StubbUnity.StubbFramework.Remove.Systems
{
    public sealed class RemoveEntitySystem : IEcsRunSystem
    {
        private EcsFilter<RemoveEntityComponent>.Exclude<DelayComponent> _removeFilter = null;

        public void Run()
        {
            foreach (var index in _removeFilter)
            {
                _removeFilter.GetEntity(index).Destroy();
            }
        }
    }
}