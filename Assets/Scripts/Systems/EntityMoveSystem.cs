using Components;
using Leopotam.Ecs;
using UnityEngine;

public class EntityMoveSystem : IEcsRunSystem
{
    private EcsFilter<Movable> _filter;
    private EcsFilter<GameStopped> _gameStoppedFilter;
    public void Run()
    {
        if (_gameStoppedFilter.GetEntitiesCount() == 0)
        {
            foreach (var entity in _filter)
            {
                float entitySpeed = _filter.Get1(entity).Speed;
                Transform entityTransform = _filter.Get1(entity).Transform;
                entityTransform.Translate(Vector3.back * entitySpeed * Time.deltaTime);
            }
        }
    }
}