using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyGetter : MonoBehaviour
{
    [SerializeField] private float _radius;
    private Collider[] _overlappedColliders;
    private List<GameObject> _enemies;
    private bool _isCollided = false;

    public bool IsCollided => _isCollided;

    public List<GameObject> GetEnemiesFromShot => _enemies;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out ShotEnemyGetter shot) == false)
        {
            _isCollided = true;
            _enemies = new List<GameObject>();

            _overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (var collider in _overlappedColliders)
            {
                if (collider.TryGetComponent(out EnemyMonoBehaviour enemyBehaviour))
                {
                    _enemies.Add(collider.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
