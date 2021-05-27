using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class DefeatCheckingSystem : IEcsRunSystem
    {
        private EcsFilter<Target> _targetFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private EcsFilter<DefeatScreen> _defeatScreenFilter;
        private EcsWorld _world;
        public void Run()
        {
            EcsEntity defeat;
            if(_gameStoppedFilter.GetEntitiesCount() == 0)
            {
                foreach (var index in _targetFilter)
                {
                    Target target = _targetFilter.Get1(index);
                    if (IsCollided(target))
                    {
                        AddDefeatComponent();
                    }
                }

                bool IsCollided(Target target)
                {
                    return target.TargetField.GetComponent<TargetCollisionChecker>().IsCollided;
                }
            }
        }

        private void AddDefeatComponent()
        {
            foreach (var defeatIndex in _defeatScreenFilter)
            {
                _defeatScreenFilter.GetEntity(defeatIndex).Get<Defeat>();
            }
        }
    }
}