using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class DamageHandlingSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Shot> _shotFilter;
        private EcsFilter<Enemy> _enemyFilter;
        public void Run()
        {
            foreach (var shotIndex in _shotFilter)
            {
                ShotEnemyGetter shotEnemyGetter = _shotFilter.Get1(shotIndex).ShotView.GetComponent<ShotEnemyGetter>();

                if (shotEnemyGetter.IsCollided)
                {
                    List<GameObject> enemies = shotEnemyGetter.GetEnemiesFromShot;
                    SetDamageToEnemies(enemies, _shotFilter.Get1(shotIndex));

                    GameObject.Destroy(shotEnemyGetter.gameObject);
                    _shotFilter.GetEntity(shotIndex).Destroy();
                }
            }
        }
        
        private void SetDamageToEnemies(List<GameObject> collidedEnemies, Shot shot)
        {
            foreach (var enemyIndex in _enemyFilter)
            {
                ref Enemy enemy = ref _enemyFilter.Get1(enemyIndex);
                if (collidedEnemies.Contains(enemy.EnemyView))
                {
                    _enemyFilter.GetEntity(enemyIndex).Get<Damage>().DamageAmount = shot.ShotDamage;
                }
            }
        }
    }
}