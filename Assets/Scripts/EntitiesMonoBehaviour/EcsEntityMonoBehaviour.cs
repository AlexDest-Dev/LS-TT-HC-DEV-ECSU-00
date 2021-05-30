using Leopotam.Ecs;
using UnityEngine;

namespace EntitiesMonoBehaviour
{
    public abstract class EcsEntityMonoBehaviour : MonoBehaviour
    {
        protected EcsEntity _ecsEntity;

        public void SetEcsEntity(EcsEntity entity)
        {
            _ecsEntity = entity;
        }
    }
}
