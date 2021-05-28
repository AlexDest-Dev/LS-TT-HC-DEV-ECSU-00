using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyConfiguration", fileName = "NewEnemyConfiguration", order = 3)]
internal class EnemyConfiguration : ScriptableObject
{
    public GameObject enemyPrefab;
    public float enemyHealth;
    public float enemySpeed;
    public GameObject targetField;
}