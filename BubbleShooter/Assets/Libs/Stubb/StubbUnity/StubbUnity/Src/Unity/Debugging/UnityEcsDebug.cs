using Leopotam.Ecs;
using StubbUnity.StubbFramework.Debugging;

namespace StubbUnity.Unity.Debugging
{
    public class UnityEcsDebug : EcsDebug
    {
        public override void Init(EcsSystems rootSystems, EcsWorld world)
        {
            base.Init(rootSystems, world);
            
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (rootSystems);
#endif
        }
    }
}