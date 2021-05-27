using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class TimerUpdateSystem : IEcsRunSystem
    {
        private EcsFilter<GlobalTimer> _timerFilter; 
        public void Run()
        {
            _timerFilter.Get1(0).Time += Time.deltaTime;
        }
    }
}