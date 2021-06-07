using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Remove.Components;
using StubbUnity.StubbFramework.View.Components;

namespace StubbUnity.StubbFramework.View.Systems
{
    public sealed class RemoveEcsViewLinkSystem : IEcsRunSystem
    {
        private EcsFilter<EcsViewLinkComponent, RemoveEntityComponent>.Exclude<DelayComponent> _removeViewFilter;
            
        public void Run()
        {
            if (_removeViewFilter.IsEmpty()) return;
            
            foreach (var idx in _removeViewFilter)
            {
                var view = _removeViewFilter.Get1(idx).Value;
                view.Dispose();
            }
        }
    }
}