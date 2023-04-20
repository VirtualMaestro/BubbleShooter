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
        
        public override void OnInitialize()
        {
            _entityLevelConfig = World.NewEntity();
            _entityLevelConfig.Get<LevelConfigComponent>().LevelConfig = levelConfig;
        }

        protected override void OnDispose()
        {
            if (_entityLevelConfig.IsAlive() && _entityLevelConfig.IsWorldAlive())
                _entityLevelConfig.Destroy();
        }
    }
}