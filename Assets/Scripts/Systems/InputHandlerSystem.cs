using Components;
using Leopotam.Ecs;
using UnityEngine;
using Touch = Components.Touch;

namespace Systems
{
    public class InputHandlerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<GameStopped> _gameStoppedFilter;
        public void Run()
        {
            if (_gameStoppedFilter.IsEmpty())
            {
                for (int i = 0; i < Input.touchCount; i++)
                {

                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        Debug.Log("Touched");
                        EcsEntity touchPosition = _world.NewEntity();
                        touchPosition.Get<Touch>().Position = Input.GetTouch(i).position;
                    }
                }
            }

        }
    }
}