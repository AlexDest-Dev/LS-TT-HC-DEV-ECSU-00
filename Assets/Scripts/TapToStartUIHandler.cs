
using UnityEngine;
using UnityEngine.EventSystems;

public class TapToStartUIHandler : MonoBehaviour, IPointerDownHandler
{
    private bool _isClicked = false;

    public bool IsClicked => _isClicked;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _isClicked = true;
    }
}
