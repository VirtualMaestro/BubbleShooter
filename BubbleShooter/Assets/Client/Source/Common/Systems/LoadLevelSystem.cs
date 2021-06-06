using Client.Source.Common.Events;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Source.Common.Systems
{
    public class LoadLevelSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<LoadLevelSceneEvent> _loadLevelFilter;
        
        public void Run()
        {
            if (_loadLevelFilter.IsEmpty()) return;
            
            var level = _loadLevelFilter.Single().Level;
            _world.LoadScene(SceneNames.GetLevel(level), true);
        }
    }
}