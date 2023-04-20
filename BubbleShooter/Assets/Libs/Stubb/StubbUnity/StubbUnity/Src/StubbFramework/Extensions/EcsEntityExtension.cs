using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Destroy.Components;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class EcsEntityExtension
    {
        public static void DestroyDelayEndFrame(ref this EcsEntity entity)
        {
            entity.Get<DestroyEntityComponent>();
        }

        public static void DestroyDelay(ref this EcsEntity entity, long milliseconds)
        {
            entity.Get<DestroyEntityComponent>();
            entity.Get<DelayComponent>().Milliseconds = milliseconds;
        }
    }
}