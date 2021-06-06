using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Remove.Components
{
    /// <summary>
    /// The component is attached to an entity which should be removed at the end of the loop.
    /// </summary>
    public struct RemoveEntityComponent : IEcsIgnoreInFilter
    {}
}