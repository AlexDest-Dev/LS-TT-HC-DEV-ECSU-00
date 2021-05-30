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
            if (_gameStoppedFilter.IsEmpty())
            {
                foreach (var globalTimerIndex in _globalTimerFilter)
                {
                    foreach (var timerUIIndex in _timerUiFilter)
                    {
                        
                        float timeRemaining =
                            _worldConfiguration.RoundTimer - _globalTimerFilter.Get1(globalTimerIndex).Time;
                        if (timeRemaining < 0f)
                        {
                            _globalTimerFilter.GetEntity(globalTimerIndex).Get<TimeIsUp>();
                            timeRemaining = 0f;
                        }
                        _timerUiFilter.Get1(timerUIIndex).TimerUIView.text = 
                            "Time remaining: " + Math.Round(timeRemaining);
                    }
                }
            }
        }
    }
}