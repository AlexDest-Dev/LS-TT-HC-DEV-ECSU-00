using UnityEngine;

[CreateAssetMenu]
public class WorldConfiguration : ScriptableObject
{
    [SerializeField] private GameObject _targetField;
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private ShotType[] _shotTypes;
    [SerializeField] private float _roundTimer = 25f;
    [SerializeField] private float _shotHeight = 10f;
    [SerializeField] private float _gravityModifier = -15f;
    [SerializeField] private int _obstaclesAmount = 5;

    public float ShotHeight => _shotHeight;
    public GameObject TargetField => _targetField;
    public float RoundTimer => _roundTimer;
    public float GravityModifier => _gravityModifier;
    public int ObstaclesAmount => _obstaclesAmount;
    public GameObject ObstaclePrefab => _obstaclePrefab;


    public ShotType GetRandomShotType()
    {
        int index = Random.Range(0, _shotTypes.Length);
        return _shotTypes[index];
    }
}