using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class DefeatScreenActivatingSystem : IEcsRunSystem
    {
        private EcsFilter<Defeat> _defeatFilter;
        public void Run()
        {
            foreach (var defeatIndex in _defeatFilter)
            {
                EcsEntity defeatEntity = _defeatFilter.GetEntity(defeatIndex);
                defeatEntity.Get<DefeatScreen>().DefeatScreenView.SetActive(true);
                defeatEntity.Get<GameStopped>();
            }
        }
    }
}