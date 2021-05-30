using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using EntitiesMonoBehaviour;
using Leopotam.Ecs;
using UnityEngine;

public class TargetEntityMonoBehaviour : EcsEntityMonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyEntityMonoBehaviour enemy))
        {
            _ecsEntity.Get<Collided>();
        }
    }
}
