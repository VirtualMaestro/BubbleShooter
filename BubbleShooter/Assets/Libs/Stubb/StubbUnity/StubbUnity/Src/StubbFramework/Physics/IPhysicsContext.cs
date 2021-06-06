using StubbUnity.StubbFramework.Core;

namespace StubbUnity.StubbFramework.Physics
{
    /// <summary>
    /// Context which is implemented this interface will be processed only on PhysicsUpdate loop (e.g. for Unity FixedUpdate).
    /// </summary>
    public interface IPhysicsContext : IStubbContext
    {
    }
}