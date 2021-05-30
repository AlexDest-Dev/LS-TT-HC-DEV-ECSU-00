using Components;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class FiringSystem : IEcsRunSystem
    {
        private EcsFilter<HitPoint> _hitPointFilter;
        private EcsWorld _world;
        private WorldConfiguration _worldConfiguration;
        public void Run()
        {
            foreach (var index in _hitPointFilter)
            {
                CreateShot(_hitPointFilter.Get1(index).Position);
            }
        }

        private void CreateShot(Vector3 position)
        {
            GameObject shotView = GameObject.Instantiate(_worldConfiguration.bombPrefab, 
                    new Vector3(position.x, _worldConfiguration.bombHeight, position.z), Quaternion.identity);
            EcsEntity shotEntity = _world.NewEntity();
            shotView.GetComponent<BombEntityMonoBehaviour>().SetEcsEntity(shotEntity);
            ref Shot shotComponent = ref shotEntity.Get<Shot>();
            shotComponent.ShotView = shotView;
            shotComponent.ShotDamage = _worldConfiguration.bombDamage;
        }
    }
}