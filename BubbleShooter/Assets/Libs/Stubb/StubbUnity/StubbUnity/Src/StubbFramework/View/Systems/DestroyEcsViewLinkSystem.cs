using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Destroy.Components;
using StubbUnity.StubbFramework.View.Components;

namespace StubbUnity.StubbFramework.View.Systems
{
    public sealed class DestroyEcsViewLinkSystem : IEcsRunSystem
    {
        private EcsFilter<EcsViewLinkComponent, DestroyEntityComponent>.Exclude<DelayComponent> _destroyViewFilter;
            
        public void Run()
        {
            if (_destroyViewFilter.IsEmpty()) return;
            
            foreach (var idx in _destroyViewFilter)
            {
                var view = _destroyViewFilter.Get1(idx).Value;
                view.Destroy();
            }
        }
    }
}