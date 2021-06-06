using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.StubbFramework.Scenes.Events;
using StubbUnity.StubbFramework.Scenes.Systems;

namespace StubbUnity.StubbFramework.Scenes
{
    public class SceneFeature : EcsFeature
    {
        public SceneFeature(EcsWorld world, string name = null, bool isEnable = true) : base(world, name, isEnable)
        {
            Add(new ProcessScenesSystem());

            OneFrame<SceneBecomeActiveComponent>();
            OneFrame<SceneBecomeInactiveComponent>();

            Add(new ActivateSceneSystem());
            Add(new DeactivateSceneSystem());

            OneFrame<ScenesSetLoadingCompleteEvent>();
            OneFrame<ScenesSetUnloadingCompleteEvent>();
            OneFrame<SceneUnloadingCompleteEvent>();
            OneFrame<SceneReadyComponent>();
            
            OneFrame<ProcessScenesEvent>();
            OneFrame<ActivateSceneEvent>();
            OneFrame<DeactivateSceneEvent>();
        }
    }
}