using Client.Source.Common;
using Client.Source.Common.Events;
using Client.Source.Gameplay.Events;
using Client.Source.Gameplay.Systems;
using Client.Source.SOConfigs;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Core;
using StubbUnity.StubbFramework.Logging;
using StubbUnity.StubbFramework.Scenes.Events;

namespace Client.Source.Gameplay
{
    public class GameplayFeature : EcsFeature, IEcsRunSystem
    {
        private readonly AppSettings _settings = null;
        private EcsFilter<SceneActivatedEvent> _sceneVisibleFilter;
        private EcsFilter<SceneDeactivatedEvent> _sceneInvisibleFilter;
        
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
            Add(new ShowDestroyParticleSystem());
            Add(new CheckWinSystem());
            
            OneFrame<StartDrawTrajectoryEvent>();
            OneFrame<ShootCoinEvent>();
            OneFrame<RefillGunCoinsEvent>();
            OneFrame<DestroyCoinViewEvent>();
            OneFrame<BoostIconClickEvent>();
            OneFrame<RemoveClusterEvent>();
            OneFrame<CoinsRemovedEvent>();
            OneFrame<ShowDestroyParticleEvent>();
        }

        public void Run()
        {
            if (!_sceneVisibleFilter.IsEmpty())
            {
                foreach (var idx in _sceneVisibleFilter)
                {
                    var sceneName = _sceneVisibleFilter.Get1(idx).Scene.SceneName;
                
                    if (sceneName.Equals(SceneNames.GetLevel(_settings.currentLevel)))
                    {
                        log.Info($"Scene ready: {sceneName}");
                        Enable = true;

                        World.NewEntity().Get<BuildLevelEvent>();
                        
                        break;
                    }
                }
            }

            if (!_sceneInvisibleFilter.IsEmpty())
            {
                foreach (var idx in _sceneInvisibleFilter)
                {
                    var sceneName = _sceneInvisibleFilter.Get1(idx).Scene.SceneName;
                    
                    if (sceneName.Equals(SceneNames.GetLevel(_settings.currentLevel)))
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