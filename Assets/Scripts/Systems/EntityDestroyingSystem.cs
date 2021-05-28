using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class EntityDestroyingSystem : IEcsRunSystem
    {
        private EcsFilter<Enemy, Destroy> _destroyedEnemyFilter;
        public void Run()
        {
            DestroyEnemy();
        }

        private void DestroyEnemy()
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