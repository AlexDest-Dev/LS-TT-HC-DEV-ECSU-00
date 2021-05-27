using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New UI Configuration", menuName = "Data/UIConfiguration", order = 3)]
public class UIConfiguration : ScriptableObject
{
    public Canvas rootCanvasPrefab;
    public Text timerTextPrefab;
    public GameObject tapToStart;
    public GameObject defeatScreenPrefab;
}