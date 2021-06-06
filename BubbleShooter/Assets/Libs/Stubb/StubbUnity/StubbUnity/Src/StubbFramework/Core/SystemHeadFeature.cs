using Leopotam.Ecs;
using StubbUnity.StubbFramework.Delay.Systems;
using StubbUnity.StubbFramework.Time.Systems;

namespace StubbUnity.StubbFramework.Core
{
    public class SystemHeadFeature : EcsFeature
    {
        public SystemHeadFeature(EcsWorld world, string name = "HeadSystems") : base(world, name)
        {
            Add(new TimeSystem());
            Add(new DelaySystem());
        }
    }
}