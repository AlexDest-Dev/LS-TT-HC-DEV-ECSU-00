using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class WorldInitializingSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private WorldConfiguration _worldConfiguration;
        private SpawnConfiguration _spawnConfiguration;
    
        public void Init()
        {
            CreateTargetField();
            
            CreateSpawnPoints();

            CreateGlobalTimer();

            UpdateGravityParameter();
            
            CreateLastSpawnTime();
            
            CreateBeforeStartEntity();
        }

        private void CreateSpawnPoints()
        {
            foreach (var spawnPointPosition in _spawnConfiguration.spawnPointsPositions)
            {
                GameObject spawnerPointView = 
                    GameObject.Instantiate(_spawnConfiguration.spawnPointPrefab, spawnPointPosition, Quaternion.identity);
                
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
            Vector3 gravityVector = new Vector3(Physics.gravity.x, _worldConfiguration.gravityModifier, Physics.gravity.z);
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
            GameObject targetField = GameObject.Instantiate(_worldConfiguration.targetField);

            EcsEntity target = _world.NewEntity();
            target.Get<Target>().TargetField = targetField;
        }
    }
}