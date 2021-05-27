using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    internal class CreatingSpawnPointsSystem : IEcsInitSystem
    {
        private SpawnConfiguration _spawnPoints;
        private EcsWorld _world;
    
        public void Init()
        {
            foreach (var spawnPointPosition in _spawnPoints.spawnPointsPositions)
            {
                GameObject spawnerPointView = 
                    GameObject.Instantiate(_spawnPoints.spawnPointPrefab, spawnPointPosition, Quaternion.identity);
                
                EcsEntity spawnPoint = _world.NewEntity();
                
                ref Spawner spawner = ref spawnPoint.Get<Spawner>();

                spawner.SpawnerView = spawnerPointView;
            }
        }
    }
}