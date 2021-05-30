using Components;
using Leopotam.Ecs;
using UnityEngine.SceneManagement;

namespace Systems
{
    public class LevelsLoadSystem : IEcsRunSystem
    {
        private EcsFilter<GameStopped, FinishScreen, Clicked> _finishedGameFilter;
        private LevelsConfiguration _levelsConfiguration;
    
        public void Run()
        {
            foreach (var finishedGameIndex in _finishedGameFilter)
            {
                EcsEntity finishedGame = _finishedGameFilter.GetEntity(finishedGameIndex);
                LoadLevel(finishedGame);
            }
        }

        private void LoadLevel(EcsEntity finishedGame)
        {
            if (finishedGame.Has<Victory>())
            {
                SceneManager.LoadScene(_levelsConfiguration.GetNextLevelName());
            }
            else
            {
                SceneManager.LoadScene(_levelsConfiguration.GetCurrentLevelName());
            }
        }
    }
}