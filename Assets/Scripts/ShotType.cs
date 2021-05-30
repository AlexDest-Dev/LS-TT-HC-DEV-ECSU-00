using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New ShotType", menuName = "Data/ShotType", order = 5)]
public class ShotType : ScriptableObject
{
    [SerializeField] private GameObject _shotPrefab;
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _damageDistanceModifier;
    [SerializeField] private float _explosionForce;

    public GameObject ShotPrefab => _shotPrefab;
    public float Damage => _damage;
    public float Radius => _radius;
    public float DistanceModifier => _damageDistanceModifier;
    public float ExplosionForce => _explosionForce;
}
