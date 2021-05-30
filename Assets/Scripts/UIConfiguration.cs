using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New UI Configuration", menuName = "Data/UIConfiguration", order = 3)]
public class UIConfiguration : ScriptableObject
{
    [SerializeField] private Canvas _rootCanvasPrefab;
    [SerializeField] private Text _timerTextPrefab;
    [SerializeField] private GameObject _tapToStart;
    [SerializeField] private GameObject _defeatScreenPrefab;
    [SerializeField] private GameObject _victoryScreenPrefab;
    public Canvas RootCanvasPrefab => _rootCanvasPrefab;
    public Text TimerTextPrefab => _timerTextPrefab;
    public GameObject TapToStart => _tapToStart;
    public GameObject DefeatScreenPrefab => _defeatScreenPrefab;
    public GameObject VictoryScreenPrefab => _victoryScreenPrefab;
}