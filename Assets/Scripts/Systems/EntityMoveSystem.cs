using Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

public class EntityMoveSystem : IEcsRunSystem
{
    private EcsFilter<Movable, NavigationMeshAgent> _navMeshMovableFilter;
    private EcsFilter<Target> _targetFilter;
    private EcsFilter<GameStopped> _gameStoppedFilter;
    public void Run()
    {
        MoveNavMeshAgentsEntities();
    }

    private void MoveNavMeshAgentsEntities()
    {
        foreach (var entityIndex in _navMeshMovableFilter)
        {
            NavMeshAgent enemyAgent =
                _navMeshMovableFilter.Get1(entityIndex).Transform.GetComponentInChildren<NavMeshAgent>();
            if (_gameStoppedFilter.GetEntitiesCount() == 0)
            {
                EcsEntity entity = _navMeshMovableFilter.GetEntity(entityIndex);
                float entitySpeed = _navMeshMovableFilter.Get1(entityIndex).Speed;
                enemyAgent.speed = entitySpeed;
                enemyAgent.SetDestination(_targetFilter.Get1(0).TargetField.transform.position);
            }
            else
            {
                enemyAgent.isStopped = true;
            }
        }
    }
}