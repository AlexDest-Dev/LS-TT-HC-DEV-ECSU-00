using System;
using Components;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class ExplosionHandlingSystem : IEcsRunSystem
    {
        private EcsFilter<Shot, Collided> _collidedShotFilter;
        
        public void Run()
        {
            foreach (var shotIndex in _collidedShotFilter)
            {
                ref Shot shotComponent = ref _collidedShotFilter.Get1(shotIndex);
                ShotEntityMonoBehaviour shot = shotComponent.ShotView.GetComponent<ShotEntityMonoBehaviour>();

                Collider[] colliders = Physics.OverlapSphere(shot.transform.position, shotComponent.Radius);

                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out ObstacleEntityMonoBehaviour obstacle))
                    {
                        collider.GetComponent<Rigidbody>().AddExplosionForce(shotComponent.ShotType.ExplosionForce, shot.transform.position, shotComponent.Radius);
                    }
                }
            }
        }
    }
}