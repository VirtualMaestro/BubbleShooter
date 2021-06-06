using Client.Source.Common;
using Client.Source.Common.Events;
using Client.Source.Common.Systems;
using Client.Source.Gameplay;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Source
{
    public class MainFeature : EcsFeature, IEcsInitSystem
    {
        private EcsWorld _world;

        public MainFeature()
        {
            Add(new UIHandlingSystem());
            Add(new GameplayFeature());

            Add(new LoadLevelSystem());
            Add(new LoadLoseGameSystem());
            Add(new LoadWinGameSystem());

            OneFrame<LoadLevelSceneEvent>();
            OneFrame<BuildLevelEvent>();
            OneFrame<GameWinEvent>();
            OneFrame<GameLoseEvent>();
        }

        public void Init()
        {
            _world.LoadScene(SceneNames.MainMenuSceneName, true);
        }
    }
}