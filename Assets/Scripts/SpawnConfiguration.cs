using UnityEngine;

[CreateAssetMenu(menuName = "Data/SpawnPoints", fileName = "NewSpawnPoints", order = 2)]
class SpawnConfiguration : ScriptableObject
{
    [SerializeField] private GameObject _spawnPointPrefab;
    [SerializeField] private Vector3[] _spawnPointsPositions;
    [SerializeField] private float _spawnTimer = 1.5f;
    
    public Vector3[] SpawnPointsPositions => _spawnPointsPositions;
    public GameObject SpawnPointPrefab => _spawnPointPrefab;
    public float SpawnTimer => _spawnTimer;
}