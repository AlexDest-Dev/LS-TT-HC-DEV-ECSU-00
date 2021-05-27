using UnityEngine;

[CreateAssetMenu(menuName = "Data/SpawnPoints", fileName = "NewSpawnPoints", order = 2)]
class SpawnConfiguration : ScriptableObject
{
    public Vector3[] spawnPointsPositions;
    public GameObject spawnPointPrefab;
    public float spawnTimer = 1f;
}