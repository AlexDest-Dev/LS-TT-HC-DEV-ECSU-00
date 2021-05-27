using UnityEngine;

[CreateAssetMenu]
public class WorldConfiguration : ScriptableObject
{
    public GameObject targetField;
    public float roundTimer;
    public float bombHeight = 10f;
    public GameObject bombPrefab;
    public float gravityModifier = -15f;
}