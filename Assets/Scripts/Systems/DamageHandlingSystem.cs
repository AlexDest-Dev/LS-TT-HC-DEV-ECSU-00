using System.Collections.Generic;
using Components;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class DamageHandlingSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Shot, Collided>.Exclude<Destroy> _shotFilter;
        private EcsFilter<Enemy> _enemyFilter;
        private WorldConfiguration _worldConfiguration;
        public void Run()
        {
            foreach (var shotIndex in _shotFilter)
            {
                BombEntityMonoBehaviour shotEnemyGetter = 
                    _shotFilter.Get1(shotIndex).ShotView.GetComponent<BombEntityMonoBehaviour>();

                List<EnemyEntityMonoBehaviour> enemies = shotEnemyGetter.GetEnemiesFromShot;
                SetDamageToEnemies(enemies, _shotFilter.Get1(shotIndex));

                shotEnemyGetter.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
            }
        }
        
        private void SetDamageToEnemies(List<EnemyEntityMonoBehaviour> collidedEnemies, Shot shot)
        {
            foreach (var enemyIndex in _enemyFilter)
            {
                ref Enemy enemy = ref _enemyFilter.Get1(enemyIndex);
                if (collidedEnemies.Contains(enemy.EnemyView.GetComponentInChildren<EnemyEntityMonoBehaviour>()))
                {
                    Vector3 enemyPosition = enemy.EnemyView.transform.position;
                    Vector3 shotPosition = shot.ShotView.transform.position;
                    float damageDistanceModifier = _worldConfiguration.bombDamageDistanceReduce;
                    float receivedDamage = 
                        CalculateDistanceDamage(enemyPosition, shotPosition, shot.ShotDamage, damageDistanceModifier);
                    _enemyFilter.GetEntity(enemyIndex).Get<Damage>().DamageAmount = receivedDamage;
                    Debug.Log(receivedDamage);

                }
            }
        }

        private float CalculateDistanceDamage(Vector3 enemyPosition, Vector3 shotPosition, float damageAmount, float damageModifier)
        {
            float distance = Vector3.Distance(enemyPosition, shotPosition);
            Debug.Log("Distance: " + distance);
            int distanceDamageModifier = Mathf.RoundToInt(distance / damageModifier);
            if (distanceDamageModifier == 0)
            {
                return damageAmount;
            }
            else
            {
                return Mathf.RoundToInt(damageAmount / distanceDamageModifier);
            }
        }
    }
}