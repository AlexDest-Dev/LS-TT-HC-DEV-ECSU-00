using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class DefeatCheckingSystem : IEcsRunSystem
    {
        private EcsFilter<Target, Collided> _targetFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private EcsFilter<RootCanvas> _rootCanvasFilter;
        private UIConfiguration _uiConfiguration;
        private EcsWorld _world;
        public void Run()
        {
            if(_gameStoppedFilter.IsEmpty() && _targetFilter.IsEmpty() == false)
            {
                CreateDefeatFinishScreen();
            }
        }

        private void CreateDefeatFinishScreen()
        {
            Transform rootCanvasTransform = _rootCanvasFilter.Get1(0).RootCanvasView.transform;
            GameObject defeatFinishScreenView =
                GameObject.Instantiate(_uiConfiguration.DefeatScreenPrefab, rootCanvasTransform);
            defeatFinishScreenView.SetActive(false);
            
            EcsEntity defeatFinishScreen = _world.NewEntity();
            defeatFinishScreenView.GetComponentInChildren<ButtonEntityMonoBehaviour>().SetEcsEntity(defeatFinishScreen);
            defeatFinishScreen.Get<FinishScreen>().FinishScreenView = defeatFinishScreenView;
            defeatFinishScreen.Get<GameStopped>();
            defeatFinishScreen.Get<Defeat>();
        }
    }
}