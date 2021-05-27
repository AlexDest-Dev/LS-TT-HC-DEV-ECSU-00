using System;
using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class TimerUIUpdateSystem : IEcsRunSystem
    {
        private EcsFilter<GlobalTimer> _globalTimerFilter;
        private EcsFilter<TimerUI> _timerUiFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private WorldConfiguration _worldConfiguration;
        public void Run()
        {
            if (_gameStoppedFilter.GetEntitiesCount() == 0)
            {
                foreach (var globalTimerIndex in _globalTimerFilter)
                {
                    foreach (var timerUIIndex in _timerUiFilter)
                    {
                        float timeRemaining =
                            Math.Abs(_worldConfiguration.roundTimer - _globalTimerFilter.Get1(globalTimerIndex).Time);
                        _timerUiFilter.Get1(timerUIIndex).TimerUIView.text = Math.Round(timeRemaining).ToString();
                    }
                }
            }
        }
    }
}