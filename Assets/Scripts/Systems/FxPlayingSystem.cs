using Components;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;

public class FxPlayingSystem : IEcsRunSystem
{
    private EcsFilter<Shot, Collided> _collidedShotFilter;
    public void Run()
    {
        foreach (var shotIndex in _collidedShotFilter)
        {
            GameObject shotView = _collidedShotFilter.Get1(shotIndex).ShotView;
            GameObject shotMain = shotView.GetComponent<BombEntityMonoBehaviour>().gameObject;
            
            ParticleSystem bombParticle = shotMain.GetComponentInChildren<ParticleSystem>();
            Debug.Log(bombParticle.name);
            bombParticle.Play();

            _collidedShotFilter.GetEntity(shotIndex).Get<FxPlaying>();
        }
    }
}