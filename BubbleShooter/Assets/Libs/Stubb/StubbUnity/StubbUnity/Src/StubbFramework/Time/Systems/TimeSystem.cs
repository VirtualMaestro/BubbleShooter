using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Time.Systems
{
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