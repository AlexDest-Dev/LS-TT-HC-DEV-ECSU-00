using System;
using Components;
using Leopotam.Ecs;

namespace EntitiesMonoBehaviour
{
    public class ObstacleEntityMonoBehaviour : EcsEntityMonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}