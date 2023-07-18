using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SubatomicCanvas.View
{
    public class RaycastTargetView : MonoBehaviour, IDragHandler, IScrollHandler
    {
        public UnityEvent<Vector2> onGrab = new();
        public UnityEvent<float> onZoom = new();
        
        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Middle) return;

            var dragDelta = eventData.delta * 0.001f;
            
            onGrab.Invoke(dragDelta);
        }

        public void OnScroll(PointerEventData eventData)
        {
            var zoomRatio = 1f + eventData.scrollDelta.y * 0.1f;
            
            onZoom.Invoke(zoomRatio);
        }
    }
}