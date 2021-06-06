using Client.Source.Common.Components;
using Client.Source.SOConfigs;
using Leopotam.Ecs;
using StubbUnity.Unity.Scenes;
using UnityEngine;

namespace Client.Source.Common.Mono
{
    public class SceneLevelController : SceneController
    {
        [SerializeField] 
        private LevelConfig levelConfig;

        private EcsEntity _entityLevelConfig;
        
        public override void Initialize()
        {
            _entityLevelConfig = World.NewEntity();
            _entityLevelConfig.Get<LevelConfigComponent>().LevelConfig = levelConfig;
        }

        public override void Dispose()
        {
            if (_entityLevelConfig.IsAlive() && _entityLevelConfig.IsWorldAlive())
                _entityLevelConfig.Destroy();
        }
    }
}