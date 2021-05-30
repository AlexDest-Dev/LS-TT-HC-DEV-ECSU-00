using Leopotam.Ecs;
using UnityEngine.EventSystems;

namespace EntitiesMonoBehaviour
{
    public class TapToStartEntityMonoBehaviour : EcsEntityMonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            _ecsEntity.Get<Clicked>();
        }
    }
}