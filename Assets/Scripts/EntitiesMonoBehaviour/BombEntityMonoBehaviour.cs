using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace EntitiesMonoBehaviour
{
    public class BombEntityMonoBehaviour : EcsEntityMonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out BombEntityMonoBehaviour bombEntity) == false)
            {
                _ecsEntity.Get<Collided>();
            }
        }
    }
}