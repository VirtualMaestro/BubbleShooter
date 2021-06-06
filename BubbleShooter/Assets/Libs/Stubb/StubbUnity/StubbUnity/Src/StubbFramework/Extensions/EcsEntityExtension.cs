using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Components;
using StubbUnity.StubbFramework.Remove.Components;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class EcsEntityExtension
    {
        public static void DestroyDelayEndFrame(ref this EcsEntity entity)
        {
            entity.Get<RemoveEntityComponent>();
        }

        public static void DestroyDelay(ref this EcsEntity entity, long milliseconds)
        {
            entity.Get<RemoveEntityComponent>();
            entity.Get<DelayComponent>().Milliseconds = milliseconds;
        }
    }
}