using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class VictoryCheckingSystem : IEcsRunSystem
    {
        private EcsFilter<GlobalTimer> _roundTimerFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private EcsFilter<RootCanvas> _rootCanvasFilter;
        private WorldConfiguration _worldConfiguration;
        private UIConfiguration _uiConfiguration;
        private EcsWorld _world;
        public void Run()
        {
            if (_gameStoppedFilter.GetEntitiesCount() == 0)
            {
                GlobalTimer currentGlobalTimer = _roundTimerFilter.Get1(0);
                if (currentGlobalTimer.Time >= _worldConfiguration.roundTimer)
                {
                    CreateVictoryFinishScreen();
                }
            }
        }

        private void CreateVictoryFinishScreen()
        {
            Transform rootCanvasTransform = _rootCanvasFilter.Get1(0).RootCanvasView.transform;
            GameObject finishScreenView =
                GameObject.Instantiate(_uiConfiguration.victoryScreenPrefab, rootCanvasTransform);
            finishScreenView.SetActive(false);
            
            EcsEntity victoryFinishScreen = _world.NewEntity();
            finishScreenView.GetComponentInChildren<ButtonEntityMonoBehaviour>().SetEcsEntity(victoryFinishScreen);
            victoryFinishScreen.Get<FinishScreen>().FinishScreenView = finishScreenView;
            victoryFinishScreen.Get<GameStopped>();
            victoryFinishScreen.Get<Victory>();
        }
    }
}