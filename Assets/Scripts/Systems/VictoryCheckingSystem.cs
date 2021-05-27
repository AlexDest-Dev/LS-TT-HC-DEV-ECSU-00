using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class VictoryCheckingSystem : IEcsRunSystem
    {
        private EcsFilter<GlobalTimer> _roundTimerFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private WorldConfiguration _worldConfiguration;
        private EcsWorld _world;
        public void Run()
        {
            if (_gameStoppedFilter.GetEntitiesCount() == 0)
            {
                GlobalTimer currentGlobalTimer = _roundTimerFilter.Get1(0);
                EcsEntity victory;
                if (currentGlobalTimer.Time >= _worldConfiguration.roundTimer)
                {
                    victory = _world.NewEntity();
                    victory.Get<Victory>();
                    victory.Get<GameStopped>();
                }
            }
        }
    }
}