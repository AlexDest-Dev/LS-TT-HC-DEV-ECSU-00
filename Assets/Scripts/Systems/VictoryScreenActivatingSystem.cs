using System;
using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class VictoryScreenActivatingSystem : IEcsRunSystem
    {
        private EcsFilter<Victory> _victoryFilter;
        public void Run()
        {
            foreach (var screenIndex in _victoryFilter)
            {
                EcsEntity screenEntity = _victoryFilter.GetEntity(screenIndex);
                screenEntity.Get<GameStopped>();
                screenEntity.Get<VictoryScreen>().VictoryScreenView.SetActive(true);
            }
        }
    }
}