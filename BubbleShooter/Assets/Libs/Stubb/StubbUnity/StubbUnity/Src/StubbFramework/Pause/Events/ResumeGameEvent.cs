using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Pause.Events
{
    /// <summary>
    /// Dispatches when game needs to be resumed.
    /// Use EcsWold.ResumeGame
    /// This is not the same as ApplicationPauseOnEvent (end similar). This can be applied during a gameplay.
    /// </summary>
    public struct ResumeGameEvent : IEcsIgnoreInFilter
    { }
}