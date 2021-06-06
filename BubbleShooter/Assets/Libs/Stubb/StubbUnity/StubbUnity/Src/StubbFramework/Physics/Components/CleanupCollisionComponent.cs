using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Physics.Components
{
    /// <summary>
    /// For internal use.
    /// It is added to all collision component automatically (e.g. CollisionEnterComponent) in order to be destroyed in CleanupCollisionSystem.
    /// </summary>
    public struct CleanupCollisionComponent : IEcsIgnoreInFilter
    {
    }
}