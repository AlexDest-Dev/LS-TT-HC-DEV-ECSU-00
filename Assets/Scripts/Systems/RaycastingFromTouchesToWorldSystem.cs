using Components;
using Leopotam.Ecs;
using UnityEngine;
using Touch = Components.Touch;

namespace Systems
{
    public class RaycastingFromTouchesToWorldSystem : IEcsRunSystem
    {
        private EcsFilter<Touch> _touchPositionsFilter;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        private EcsWorld _world;
        public void Run()
        {
            if (_gameStoppedFilter.IsEmpty())
            {
                foreach (var index in _touchPositionsFilter)
                {
                    EcsEntity hitPoint;
                    Ray ray = Camera.main.ScreenPointToRay(_touchPositionsFilter.Get1(index).Position);

                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        hitPoint = _world.NewEntity();
                        hitPoint.Get<HitPoint>().Position = hit.point;
                    }
                }
            }
        }
    }
}