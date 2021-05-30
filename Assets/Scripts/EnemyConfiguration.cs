using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyConfiguration", fileName = "NewEnemyConfiguration", order = 3)]
internal class EnemyConfiguration : ScriptableObject
{
    [SerializeField] private EnemyType[] _enemyTypes;

    public EnemyType GetRandomEnemy()
    {
        int index = Random.Range(0, _enemyTypes.Length);
        return _enemyTypes[index];
    }
}