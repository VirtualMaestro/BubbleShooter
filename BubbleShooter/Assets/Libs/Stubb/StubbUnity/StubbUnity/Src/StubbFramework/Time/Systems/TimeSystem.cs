using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Time.Systems
{
#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif
    public sealed class TimeSystem : IEcsRunSystem, IEcsDestroySystem
    {
        private ITimeService _timeService;

        public void Run()
        {
            _timeService.Update();
        }

        public void Destroy()
        {
            _timeService.Destroy();
            _timeService = null;
        }
    }
}