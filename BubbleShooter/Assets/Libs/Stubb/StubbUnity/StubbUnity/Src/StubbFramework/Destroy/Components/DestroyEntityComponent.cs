using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Destroy.Components
{
    /// <summary>
    /// The component is attached to an entity which should be destroyed at the end of the loop.
    /// </summary>
    public struct DestroyEntityComponent : IEcsIgnoreInFilter
    {}
}