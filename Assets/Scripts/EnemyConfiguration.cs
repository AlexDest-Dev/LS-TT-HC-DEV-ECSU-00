using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyConfiguration", fileName = "NewEnemyConfiguration", order = 3)]
internal class EnemyConfiguration : ScriptableObject
{
    public GameObject enemyPrefab;
    public float speed;
    public GameObject targetField;
}