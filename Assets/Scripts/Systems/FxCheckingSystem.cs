using Components;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class FxCheckingSystem : IEcsRunSystem
    {
        private EcsFilter<Shot, FxChecking> _fxPlayingFilter;
        public void Run()
        {
            CheckShotsFx();
        }

        private void CheckShotsFx()
        {
            foreach (var fxIndex in _fxPlayingFilter)
            {
                ShotEntityMonoBehaviour shotEntityMonoBehaviour = _fxPlayingFilter.Get1(fxIndex).ShotView
                    .GetComponent<ShotEntityMonoBehaviour>();

                if (shotEntityMonoBehaviour.GetComponentInChildren<ParticleSystem>().IsAlive() == false)
                {
                    EcsEntity shotEntity = _fxPlayingFilter.GetEntity(fxIndex);
                    shotEntity.Get<Destroy>();
                    shotEntity.Del<FxChecking>();
                }
            }
        }
    }
}