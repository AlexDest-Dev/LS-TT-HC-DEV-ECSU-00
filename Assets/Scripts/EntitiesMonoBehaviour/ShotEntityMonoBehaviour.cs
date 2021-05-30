using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace EntitiesMonoBehaviour
{
    public class ShotEntityMonoBehaviour : EcsEntityMonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out ShotEntityMonoBehaviour bombEntity) == false)
            {
                _ecsEntity.Get<Collided>();
            }
        }
    }
}