using Leopotam.Ecs;
using StubbUnity.StubbFramework.Pause.Events;

namespace StubbUnity.StubbFramework.Extensions
{
    public static class PauseGameWorldExtension
    {
        public static void PauseGame(this EcsWorld world)
        {
            world.NewEntity().Get<PauseGameEvent>();
        }

        public static void ResumeGame(this EcsWorld world)
        {
            world.NewEntity().Get<ResumeGameEvent>();
        }
    }
}