using Systems;
using Components;
using Leopotam.Ecs;
using UnityEngine;
using Touch = Components.Touch;

sealed class EcsStartup : MonoBehaviour {
    private EcsWorld _world;
    private EcsSystems _systems;
    [SerializeField] private SpawnConfiguration _spawnPoints;
    [SerializeField] private EnemyConfiguration _enemyConfiguration;
    [SerializeField] private WorldConfiguration _worldConfiguration;
    [SerializeField] private UIConfiguration _uiConfiguration;

    void Start () {
        // void can be switched to IEnumerator for support coroutines.
            
        _world = new EcsWorld ();
        _systems = new EcsSystems (_world);
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
        _systems
            // register your systems here, for example:
            .Add(new WorldInitializingSystem())
            .Add(new CreatingSpawnPointsSystem())
            .Add(new UIInitializingSystem())
            .Add(new TimerUIUpdateSystem())
            .Add(new InputHandlerSystem())
            .Add(new TapToStartTrackingSystem())
            .Add(new RaycastingFromTouchesToWorldSystem())
            .Add(new FiringSystem())
            .Add(new EnemyDestroyingSystem())
            .Add(new EnemySpawnSystem())
            .Add(new EntityMoveSystem())
            .Add(new DefeatCheckingSystem())
            .Add(new DefeatScreenActivatingSystem())
            .Add(new VictoryCheckingSystem())
            .Add(new VictoryScreenActivatingSystem())
            .Add(new TimerUpdateSystem())
            // register one-frame components (order is important), for example:
            // .OneFrame<TestComponent1> ()
            // .OneFrame<TestComponent2> ()
            .OneFrame<Touch>()
            .OneFrame<HitPoint>()
            // inject service instances here (order doesn't important), for example:
            .Inject(_spawnPoints)
            .Inject(_enemyConfiguration)
            .Inject(_worldConfiguration)
            .Inject(_uiConfiguration)
            .Init();
    }

    void Update () {
        _systems?.Run();
    }

    void OnDestroy () {
        if (_systems != null) {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}