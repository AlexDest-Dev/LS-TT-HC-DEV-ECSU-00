using System;
using Components;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;

public class EnemySpawnSystem : IEcsRunSystem
{
    private EcsFilter<Spawner> _spawnerFilter;
    private EcsFilter<GlobalTimer> _timerFilter;
    private EcsFilter<LastSpawnTime> _lastTimeSpawnFilter;
    private EcsFilter<GameStopped> _gameStoppedFilter;
    private EcsFilter<TimeIsUp> _timeIsUpFilter;
    private EnemyConfiguration _enemyConfiguration;
    private SpawnConfiguration _spawnConfiguration;
    private EcsWorld _world;
    
    public void Run()
    {
        if (_gameStoppedFilter.IsEmpty() && _timeIsUpFilter.IsEmpty())
        {
            ref GlobalTimer globalTimer = ref _timerFilter.Get1(0);
            ref LastSpawnTime lastSpawnTime = ref _lastTimeSpawnFilter.Get1(0);
            
            if (Math.Abs(globalTimer.Time - lastSpawnTime.Time) >= _spawnConfiguration.SpawnTimer)
            {
                int randomIndexSpawner = UnityEngine.Random.Range(0, _spawnerFilter.GetEntitiesCount());

                EcsEntity spawnerEntity = _spawnerFilter.GetEntity(randomIndexSpawner);
                spawnerEntity.Get<FxPlaying>();
                Vector3 spawnPosition = spawnerEntity.Get<Spawner>().SpawnerView.transform.position;
                CreateEnemy(spawnPosition);
                
                lastSpawnTime.Time = globalTimer.Time;
            }
        }
    }

    private void CreateEnemy(Vector3 spawnPosition)
    {
        EnemyType enemyType = _enemyConfiguration.GetRandomEnemy();
        GameObject enemyView =
            GameObject.Instantiate(enemyType.EnemyPrefab, spawnPosition, Quaternion.identity);
        EnemyEntityMonoBehaviour enemyEntityMonoBehaviour = enemyView.GetComponentInChildren<EnemyEntityMonoBehaviour>();
        
        EcsEntity enemyEntity = _world.NewEntity();
        enemyEntityMonoBehaviour.SetEcsEntity(enemyEntity);
        enemyEntity.Get<Enemy>().EnemyView = enemyView;
        
        ref Health enemyHealth = ref enemyEntity.Get<Health>();
        enemyHealth.MaxHealthAmount = enemyType.EnemyHealth;
        enemyHealth.CurrentHealthAmount = enemyHealth.MaxHealthAmount;
        
        ref Movable enemyMovable = ref enemyEntity.Get<Movable>();
        enemyMovable.Speed = enemyType.EnemySpeed;
        enemyMovable.Transform = enemyView.transform;

        enemyEntity.Get<NavigationMeshAgent>();
    }
}