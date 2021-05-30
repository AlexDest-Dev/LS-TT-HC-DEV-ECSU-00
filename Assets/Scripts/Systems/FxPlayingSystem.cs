using Components;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;

public class FxPlayingSystem : IEcsRunSystem
{
    private EcsFilter<Shot, FxPlaying> _collidedShotFilter;
    private EcsFilter<Spawner, FxPlaying> _spawnerFilter;
    public void Run()
    {
        PlaySpawnerFx();

        PlayShotFx();
    }

    private void PlaySpawnerFx()
    {
        foreach (var spawnerIndex in _spawnerFilter)
        {
            GameObject spawnerView = _spawnerFilter.Get1(spawnerIndex).SpawnerView;
            spawnerView.GetComponent<ParticleSystem>().Play();
        }
    }

    private void PlayShotFx()
    {
        foreach (var shotIndex in _collidedShotFilter)
        {
            GameObject shotView = _collidedShotFilter.Get1(shotIndex).ShotView;
            GameObject shotMain = shotView.GetComponent<BombEntityMonoBehaviour>().gameObject;

            ParticleSystem bombParticle = shotMain.GetComponentInChildren<ParticleSystem>();
            Debug.Log(bombParticle.name);
            bombParticle.Play();

            EcsEntity shotEntity = _collidedShotFilter.GetEntity(shotIndex);
            shotEntity.Get<FxChecking>();
        }
    }
}