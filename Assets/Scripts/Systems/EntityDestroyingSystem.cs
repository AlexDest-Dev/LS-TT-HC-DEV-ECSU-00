using System.Collections.Generic;
using System.Security.Cryptography;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class EntityDestroyingSystem : IEcsRunSystem
    {
        private EcsFilter<Enemy, Destroy> _destroyedEnemyFilter;
        private EcsFilter<Shot, Destroy> _destroyedShotFilter;
        public void Run()
        {
            DestroyShots();

            DestroyEnemies();
        }

        private void DestroyShots()
        {
            foreach (var shotIndex in _destroyedShotFilter)
            {
                EcsEntity shotEntity = _destroyedShotFilter.GetEntity(shotIndex);
                GameObject.Destroy(shotEntity.Get<Shot>().ShotView);
                shotEntity.Destroy();
            }
        }

        private void DestroyEnemies()
        {
            foreach (var enemyIndex in _destroyedEnemyFilter)
            {
                EcsEntity enemy = _destroyedEnemyFilter.GetEntity(enemyIndex);
                GameObject.Destroy(enemy.Get<Enemy>().EnemyView);
                enemy.Destroy();
            }
        }
    }
}