using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class WorldInitializingSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private WorldConfiguration _worldConfiguration;
    
        public void Init()
        {
            CreateTargetField();

            CreateGlobalTimer();

            UpdateGravityParameter();
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