using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class VictoryCheckingSystem : IEcsRunSystem
    {
        private EcsFilter<GlobalTimer> _roundTimerFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private EcsFilter<VictoryScreen> _victoryScreenFilter;
        private WorldConfiguration _worldConfiguration;
        private EcsWorld _world;
        public void Run()
        {
            if (_gameStoppedFilter.GetEntitiesCount() == 0)
            {
                GlobalTimer currentGlobalTimer = _roundTimerFilter.Get1(0);
                if (currentGlobalTimer.Time >= _worldConfiguration.roundTimer)
                {
                    foreach (var screenIndex in _victoryScreenFilter)
                    {
                        _victoryScreenFilter.GetEntity(screenIndex).Get<Victory>();
                        
                    }
                }
            }
        }
    }
}