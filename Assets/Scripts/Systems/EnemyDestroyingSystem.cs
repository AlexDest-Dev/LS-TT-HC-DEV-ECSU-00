using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class EnemyDestroyingSystem : IEcsRunSystem
    {
        private EcsFilter<Shot> _shotFilter;
        private EcsFilter<Enemy> _enemyFilter;
        public void Run()
        {
            foreach (var shot in _shotFilter)
            {
                ShotEnemyGetter shotEnemyGetter = _shotFilter.Get1(shot).ShotView.GetComponent<ShotEnemyGetter>();

                if (shotEnemyGetter.IsCollided)
                {
                    List<GameObject> enemies = shotEnemyGetter.GetEnemiesFromShot;
                    DestroyEnemies(enemies);

                    GameObject.Destroy(shotEnemyGetter.gameObject);
                    _shotFilter.GetEntity(shot).Destroy();
                }
            }
        }

        private void DestroyEnemies(List<GameObject> enemies)
        {
            foreach (var enemyIndex in _enemyFilter)
            {
                ref Enemy enemy = ref _enemyFilter.Get1(enemyIndex);
                if (enemies.Contains(enemy.EnemyView))
                {
                    GameObject.Destroy(enemy.EnemyView);
                    _enemyFilter.GetEntity(enemyIndex).Destroy();
                }
            }
        }
    }
}