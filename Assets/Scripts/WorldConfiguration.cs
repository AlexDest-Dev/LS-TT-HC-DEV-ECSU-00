using UnityEngine;

[CreateAssetMenu]
public class WorldConfiguration : ScriptableObject
{
    public GameObject targetField;
    public float roundTimer;

    public GameObject bombPrefab;
    public float bombHeight = 10f;
    public float bombDamage = 50f;
    public float bombDamageDistanceReduce = 1f;

    public float gravityModifier = -15f;
}