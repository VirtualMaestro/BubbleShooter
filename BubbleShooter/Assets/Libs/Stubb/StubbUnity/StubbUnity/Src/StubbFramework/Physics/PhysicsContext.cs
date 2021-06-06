using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;

namespace StubbUnity.StubbFramework.Physics
{
    public class PhysicsContext : StubbContext, IPhysicsContext
    {
        public PhysicsContext(EcsWorld world) : base(world)
        {
        }

        protected override void InitFeatures()
        {
            TailFeature = new PhysicsTailFeature(World);
        }

        public override void Dispose()
        {
            RootSystems.Destroy();
            RootSystems = null;
        }
    }
}