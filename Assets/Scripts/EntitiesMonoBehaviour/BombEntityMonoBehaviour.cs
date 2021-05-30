using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace EntitiesMonoBehaviour
{
    public class BombEntityMonoBehaviour : EcsEntityMonoBehaviour
    {
        [SerializeField] private float _radius;
        private Collider[] _overlappedColliders;
        private List<EnemyEntityMonoBehaviour> _enemies;

        public float Radius => _radius;
        public List<EnemyEntityMonoBehaviour> GetEnemiesFromShot => _enemies;
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out BombEntityMonoBehaviour bombEntity) == false)
            {
                _enemies = new List<EnemyEntityMonoBehaviour>();

                _overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
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
                _ecsEntity.Get<Collided>();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
        
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}