using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class HealthModifyingSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Damage> _damagedFilter;
        private EcsFilter<Health> _healthFilter;
        public void Run()
        {
            foreach (var damageIndex in _damagedFilter)
            {
                EcsEntity damagedEntity = _damagedFilter.GetEntity(damageIndex);
                if (damagedEntity.Has<Health>())
                {
                    ref Health healthComponent = ref damagedEntity.Get<Health>();
                    healthComponent.CurrentHealthAmount -= damagedEntity.Get<Damage>().DamageAmount;
                    if (healthComponent.CurrentHealthAmount <= 0)
                    {
                        SetDestroy(damagedEntity);
                    }
                }
            }
        }

        private void SetDestroy(EcsEntity destroyingEntity)
        {
            destroyingEntity.Get<Destroy>();
        }
    }
}