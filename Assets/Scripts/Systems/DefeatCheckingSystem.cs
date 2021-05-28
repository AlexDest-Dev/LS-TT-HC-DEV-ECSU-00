using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class DefeatCheckingSystem : IEcsRunSystem
    {
        private EcsFilter<Target> _targetFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private EcsFilter<RootCanvas> _rootCanvasFilter;
        private UIConfiguration _uiConfiguration;
        private EcsWorld _world;
        public void Run()
        {
            if(_gameStoppedFilter.GetEntitiesCount() == 0)
            {
                foreach (var index in _targetFilter)
                {
                    Target target = _targetFilter.Get1(index);
                    if (IsCollided(target))
                    {
                        CreateDefeatFinishScreen();
                    }
                }

                bool IsCollided(Target target)
                {
                    return target.TargetField.GetComponent<TargetCollisionChecker>().IsCollided;
                }
            }
        }

        private void CreateDefeatFinishScreen()
        {
            Transform rootCanvasTransform = _rootCanvasFilter.Get1(0).RootCanvasView.transform;
            GameObject defeatFinishScreenView =
                GameObject.Instantiate(_uiConfiguration.defeatScreenPrefab, rootCanvasTransform);
            defeatFinishScreenView.SetActive(false);
            EcsEntity defeatFinishScreen = _world.NewEntity();
            defeatFinishScreen.Get<FinishScreen>().FinishScreenView = defeatFinishScreenView;
            defeatFinishScreen.Get<GameStopped>();
            defeatFinishScreen.Get<Defeat>();
        }
    }
}