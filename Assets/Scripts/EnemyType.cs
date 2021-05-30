using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New EnemyType", menuName = "Data/EnemyType", order = 6)]
public class EnemyType : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _enemyHealth;
    [SerializeField] private float _enemySpeed;
    

    public GameObject EnemyPrefab => _enemyPrefab;
    public float EnemyHealth => _enemyHealth;
    public float EnemySpeed => _enemySpeed;
}