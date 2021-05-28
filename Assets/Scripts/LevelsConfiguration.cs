using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New LevelConfiguration", menuName = "Data/LevelsConfiguration", order = 4)]
public class LevelsConfiguration : ScriptableObject
{
    [SerializeField] private string[] _levels;
    private int _levelPointer = 0;

    public string GetNextLevelName()
    {
        _levelPointer++;
        if (_levelPointer == _levels.Length)
        {
            _levelPointer = 0;
            return _levels[_levelPointer];
        }
        return _levels[_levelPointer];
    }

    public string GetCurrentLevelName()
    {
        return _levels[_levelPointer];
    }
}
