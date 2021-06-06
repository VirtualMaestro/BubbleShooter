using Client.Source.Common;
using Client.Source.Common.Events;
using Client.Source.Gameplay.Events;
using Client.Source.Gameplay.Systems;
using Client.Source.SOConfigs;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.StubbFramework.Scenes.Components;
using StubbUnity.StubbFramework.Scenes.Events;

namespace Client.Source.Gameplay
{
    public class GameplayFeature : EcsFeature, IEcsRunSystem
    {
        private readonly AppSettings _settings = null;
        private EcsFilter<SceneComponent, SceneReadyComponent> _sceneReadyFilter;
        private EcsFilter<SceneUnloadingCompleteEvent> _sceneUnloadedFilter;
        
        public GameplayFeature() : base(false)
        {
            Add(new BuildLevelSystem());
            Add(new GameplayInputSystem());
            Add(new DrawTrajectorySystem());
            Add(new ShootSystem());
            Add(new RefillGunCoinsSystem());
            Add(new ShootingTweenSystems());
            
            Add(new BoostIconClickSystem());
            
            Add(new CoinBombBoostSystem());
            Add(new CoinWildBoostSystem());
            Add(new CoinProcessSystem());
            Add(new RemoveClusterSystem());
            Add(new RemoveDisconnectedSystem());
            Add(new DestroyCoinViewSystem());
            Add(new CheckWinSystem());
            
            OneFrame<StartDrawTrajectoryEvent>();
            OneFrame<ShootCoinEvent>();
            OneFrame<RefillGunCoinsEvent>();
            OneFrame<DestroyCoinViewEvent>();
            OneFrame<BoostIconClickEvent>();
            OneFrame<RemoveClusterEvent>();
            OneFrame<CoinsRemovedEvent>();
        }

        public void Run()
        {
            if (!_sceneReadyFilter.IsEmpty())
            {
                foreach (var idx in _sceneReadyFilter)
                {
                    var scene = _sceneReadyFilter.Get1(idx).Scene;
                
                    if (scene.SceneName.Equals(SceneNames.GetLevel(_settings.currentLevel)))
                    {
                        log.Info($"Scene ready: {scene.SceneName}");
                        Enable = true;

                        World.NewEntity().Get<BuildLevelEvent>();
                        
                        break;
                    }
                }
            }

            if (!_sceneUnloadedFilter.IsEmpty())
            {
                foreach (var idx in _sceneUnloadedFilter)
                {
                    var sceneName = _sceneUnloadedFilter.Get1(idx).SceneName;
                    
                    if (sceneName.Equals(SceneNames.GetLevel(_settings.currentLevel).FullName))
                    {
                        log.Info($"Scene unloaded: {sceneName}");
                        Enable = false;
                        break;
                    }
                }
            }
        }
    }
}