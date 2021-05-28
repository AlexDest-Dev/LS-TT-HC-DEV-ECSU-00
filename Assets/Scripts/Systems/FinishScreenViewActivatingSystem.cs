using System;
using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class FinishScreenViewActivatingSystem : IEcsRunSystem
    {
        private EcsFilter<FinishScreen> _finishScreenFilter;
        public void Run()
        {
            foreach (var screenIndex in _finishScreenFilter)
            {
                _finishScreenFilter.Get1(screenIndex).FinishScreenView.SetActive(true);
            }
        }
    }
}