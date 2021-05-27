using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class TimerUpdateSystem : IEcsRunSystem
    {
        private EcsFilter<GlobalTimer> _timerFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        public void Run()
        {
            if (_gameStoppedFilter.IsEmpty())
            {
                _timerFilter.Get1(0).Time += Time.deltaTime;
            }
            
        }
    }
}