using Client.Source.Common.Events;
using Client.Source.SOConfigs;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;

namespace Client.Source.Common.Systems
{
    public class UIHandlingSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<EcsUiClickEvent> _clickEvents = null;
        private readonly AppSettings _settings = null;
        
        public void Run()
        {
            if (_clickEvents.IsEmpty()) return;

            foreach (var idx in _clickEvents)
            {
                ref var clickEvent = ref _clickEvents.Get1(idx);

                switch (clickEvent.WidgetName)
                {
                    case WidgetNames.MainPlayBtn:
                        _MainPlay();
                        break;                    
                    
                    case WidgetNames.WinPlayAgainBtn:
                        _WinPlayAgain();
                        break;  
                    
                    case WidgetNames.WinPlayNextBtn:
                        _WinPlayNext();
                        break;
                    
                    case WidgetNames.LosePlayAgainBtn:
                        _LosePlayAgain();
                        break;
                }
            }
        }

        private void _MainPlay()
        {
            _world.NewEntity().Get<LoadLevelSceneEvent>().Level = _settings.currentLevel;
        }

        private void _WinPlayAgain()
        {
            _world.NewEntity().Get<LoadLevelSceneEvent>().Level = _settings.currentLevel;
        }        
        
        private void _WinPlayNext()
        {
            _settings.currentLevel = 3 - _settings.currentLevel;
            _world.NewEntity().Get<LoadLevelSceneEvent>().Level = _settings.currentLevel;
        }

        private void _LosePlayAgain()
        {
            _world.NewEntity().Get<LoadLevelSceneEvent>().Level = _settings.currentLevel;
        }
    }
}