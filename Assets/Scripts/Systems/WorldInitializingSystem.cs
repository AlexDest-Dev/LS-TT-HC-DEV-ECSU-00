using System;
using Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems
{
    public class WorldInitializingSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private WorldConfiguration _worldConfiguration;
        private SpawnConfiguration _spawnConfiguration;
        private LevelsConfiguration _levelsConfiguration;
    
        public void Init()
        {
            LoadCurrentLevel();
            
            CreateTargetField();
            
            CreateSpawnPoints();

            CreateObstacles();

            CreateGlobalTimer();

            UpdateGravityParameter();
            
            CreateLastSpawnTime();
            
            CreateBeforeStartEntity();
        }

        private void CreateObstacles()
        {
            int obstaclesAmount = _worldConfiguration.ObstaclesAmount;
            Vector3 spawnPosition = new Vector3(-3, _worldConfiguration.ShotHeight, 0);
            for (int i = 0; i < obstaclesAmount; i++)
            {
                GameObject.Instantiate(_worldConfiguration.ObstaclePrefab, spawnPosition, Quaternion.identity);
                spawnPosition.x += 2;
            }
        }

        private void LoadCurrentLevel()
        {
            if (String.CompareOrdinal(SceneManager.GetActiveScene().name, _levelsConfiguration.GetCurrentLevelName()) != 0)
            {
                SceneManager.LoadScene(_levelsConfiguration.GetCurrentLevelName());
            }
        }

        private void CreateSpawnPoints()
        {
            foreach (var spawnPointPosition in _spawnConfiguration.SpawnPointsPositions)
            {
                GameObject spawnerPointView = 
                    GameObject.Instantiate(_spawnConfiguration.SpawnPointPrefab, spawnPointPosition, Quaternion.identity);
                
                EcsEntity spawnPoint = _world.NewEntity();
                
                ref Spawner spawner = ref spawnPoint.Get<Spawner>();

                spawner.SpawnerView = spawnerPointView;
            }
        }

        private void CreateBeforeStartEntity()
        {
            EcsEntity gameStart = _world.NewEntity();
            gameStart.Get<GameStopped>();
            gameStart.Get<BeforeStart>();
        }

        private void UpdateGravityParameter()
        {
            Vector3 gravityVector = new Vector3(Physics.gravity.x, _worldConfiguration.GravityModifier, Physics.gravity.z);
            Physics.gravity = gravityVector;
        }


        private void CreateGlobalTimer()
        {
            EcsEntity globalTimer = _world.NewEntity();
            globalTimer.Get<GlobalTimer>().Time = 0f;
        }

        private void CreateLastSpawnTime()
        {
            EcsEntity lastSpawnTime = _world.NewEntity();
            lastSpawnTime.Get<LastSpawnTime>().Time = 0f;
        }

        private void CreateTargetField()
        {
            GameObject targetField = GameObject.Instantiate(_worldConfiguration.TargetField);

            EcsEntity target = _world.NewEntity();
            targetField.GetComponent<TargetEntityMonoBehaviour>().SetEcsEntity(target);
            target.Get<Target>().TargetField = targetField;
        }
    }
}