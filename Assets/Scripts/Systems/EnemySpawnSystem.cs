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
    private EnemyConfiguration _enemyConfiguration;
    private SpawnConfiguration _spawnConfiguration;
    private EcsWorld _world;
    
    public void Run()
    {
        if (_gameStoppedFilter.GetEntitiesCount() == 0)
        {
            ref GlobalTimer globalTimer = ref _timerFilter.Get1(0);
            ref LastSpawnTime lastSpawnTime = ref _lastTimeSpawnFilter.Get1(0);
            
            if (Math.Abs(globalTimer.Time - lastSpawnTime.Time) >= _spawnConfiguration.spawnTimer)
            {
                int indexSpawner = UnityEngine.Random.Range(0, _spawnerFilter.GetEntitiesCount());

                Vector3 spawnPosition = _spawnerFilter.Get1(indexSpawner).SpawnerView.transform.position;
                CreateEnemy(spawnPosition);
                
                lastSpawnTime.Time = globalTimer.Time;
            }
        }
    }

    private void CreateEnemy(Vector3 spawnPosition)
    {
        GameObject enemyView =
            GameObject.Instantiate(_enemyConfiguration.enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyEntityMonoBehaviour enemyEntityMonoBehaviour = enemyView.GetComponentInChildren<EnemyEntityMonoBehaviour>();
        
        EcsEntity enemyEntity = _world.NewEntity();
        enemyEntityMonoBehaviour.SetEcsEntity(enemyEntity);
        enemyEntity.Get<Enemy>().EnemyView = enemyView;
        
        ref Health enemyHealth = ref enemyEntity.Get<Health>();
        enemyHealth.MaxHealthAmount = _enemyConfiguration.enemyHealth;
        enemyHealth.CurrentHealthAmount = enemyHealth.MaxHealthAmount;
        
        ref Movable enemyMovable = ref enemyEntity.Get<Movable>();
        enemyMovable.Speed = _enemyConfiguration.enemySpeed;
        enemyMovable.Transform = enemyView.transform;

        enemyEntity.Get<NavigationMeshAgent>();
    }
}