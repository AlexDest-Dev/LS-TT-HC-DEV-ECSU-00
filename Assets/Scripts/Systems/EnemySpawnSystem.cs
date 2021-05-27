using System;
using Components;
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

        EcsEntity enemy = _world.NewEntity();
        ref Movable enemyMovable = ref enemy.Get<Movable>();
        
        enemyMovable.Speed = _enemyConfiguration.speed;
        enemyMovable.Transform = enemyView.transform;
        
        enemy.Get<Enemy>().EnemyView = enemyView;
    }
}