using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Time;

namespace StubbUnity.StubbFramework.Delay.Systems
{
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
    public sealed class DelaySystem : IEcsRunSystem
    {
        private EcsFilter<DelayComponent> _filterDelay;
        private ITimeService _timeService;

        public void Run()
        {
            if (_filterDelay.IsEmpty()) return;

            foreach (var index in _filterDelay)
            {
                ref var delay = ref _filterDelay.Get1(index);
                delay.Frames--;
                delay.Milliseconds -= _timeService.TimeStep;

                if (delay.Frames <= 0 && delay.Milliseconds <= 0)
                {
                    _filterDelay.GetEntity(index).Del<DelayComponent>();
                }
            }
        }
    }
}