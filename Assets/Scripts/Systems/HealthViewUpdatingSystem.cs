using Components;
using Leopotam.Ecs;
using UnityEngine.UI;

namespace Systems
{
    public class HealthViewUpdatingSystem : IEcsRunSystem
    {
        private EcsFilter<Enemy, Health, Damage> _enemyHealthFilter;
        public void Run()
        {
            UpdateEnemyHealthView();
        }

        private void UpdateEnemyHealthView()
        {
            foreach (var enemyIndex in _enemyHealthFilter)
            {
                Slider enemyHpSlider = _enemyHealthFilter.Get1(enemyIndex).EnemyView.GetComponentInChildren<Slider>();
                ref Health enemyHealth = ref _enemyHealthFilter.Get2(enemyIndex);
                enemyHpSlider.value = enemyHealth.CurrentHealthAmount / enemyHealth.MaxHealthAmount;
            }
        }
    }
}