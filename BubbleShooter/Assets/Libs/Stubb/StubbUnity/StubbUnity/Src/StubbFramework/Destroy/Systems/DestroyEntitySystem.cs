using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Destroy.Components;

namespace StubbUnity.StubbFramework.Destroy.Systems
{
    public sealed class DestroyEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyEntityComponent>.Exclude<DelayComponent> _destroyEntityFilter = null;

        public void Run()
        {
            foreach (var index in _destroyEntityFilter)
            {
                _destroyEntityFilter.GetEntity(index).Destroy();
            }
        }
    }
}