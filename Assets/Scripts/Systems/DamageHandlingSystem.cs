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
        private List<EnemyEntityMonoBehaviour> _enemies;
        public void Run()
        {
            foreach (var shotIndex in _shotFilter)
            {
                ref Shot shotComponent = ref _shotFilter.Get1(shotIndex);
                ShotEntityMonoBehaviour shotEnemyGetter = 
                    shotComponent.ShotView.GetComponent<ShotEntityMonoBehaviour>();
                Transform shotTransform = shotEnemyGetter.transform;

                List<EnemyEntityMonoBehaviour> enemies = CheckEnemiesInRadiusOfShot(shotTransform, shotComponent.Radius);
                SetDamageToEnemies(enemies, _shotFilter.Get1(shotIndex));

                _shotFilter.GetEntity(shotIndex).Get<FxPlaying>();
                shotEnemyGetter.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
            }
        }

        private List<EnemyEntityMonoBehaviour> CheckEnemiesInRadiusOfShot(Transform shotTransform, float radius)
        {
            _enemies = new List<EnemyEntityMonoBehaviour>();

            Collider[] _overlappedColliders = Physics.OverlapSphere(shotTransform.position, radius);
            foreach (var collider in _overlappedColliders)
            {
                EnemyEntityMonoBehaviour enemyEntityMonoBehaviour =
                    collider.GetComponentInChildren<EnemyEntityMonoBehaviour>();
                if (enemyEntityMonoBehaviour != null)
                {
                    Debug.Log("Enemy added");
                    _enemies.Add(enemyEntityMonoBehaviour);
                }
            }

            return _enemies;
        }
        
        private void SetDamageToEnemies(List<EnemyEntityMonoBehaviour> collidedEnemies, Shot shot)
        {
            foreach (var enemyIndex in _enemyFilter)
            {
                ref Enemy enemy = ref _enemyFilter.Get1(enemyIndex);
                if (collidedEnemies.Contains(enemy.EnemyView.GetComponentInChildren<EnemyEntityMonoBehaviour>()))
                {
                    Vector3 enemyPosition = enemy.EnemyView.GetComponentInChildren<EnemyEntityMonoBehaviour>().transform.position;
                    Vector3 shotPosition = shot.ShotView.transform.position;
                    
                    float damageDistanceModifier = shot.ShotType.DistanceModifier;
                    float receivedDamage = 
                        CalculateDistanceDamage(enemyPosition, shotPosition, shot.ShotDamage, damageDistanceModifier);
                    
                    if(Vector3.Distance(enemyPosition, shotPosition) <= shot.Radius)
                    {
                        _enemyFilter.GetEntity(enemyIndex).Get<Damage>().DamageAmount = receivedDamage;
                        Debug.Log(receivedDamage);
                    }

                }
            }
        }

        private float CalculateDistanceDamage(Vector3 enemyPosition, Vector3 shotPosition, float damageAmount, float damageModifier)
        {
            float distance = Vector3.Distance(enemyPosition, shotPosition);
            Debug.Log("Distance: " + distance);
            float distanceDamageModifier = distance / damageModifier;
            return damageAmount / distanceDamageModifier;
        }
    }
}