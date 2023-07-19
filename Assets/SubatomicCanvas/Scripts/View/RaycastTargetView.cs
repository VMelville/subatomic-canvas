using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SubatomicCanvas.View
{
    public class RaycastTargetView : MonoBehaviour, IDragHandler, IScrollHandler
    {
        public UnityEvent<Vector2> onMiddleDrag = new();
        public UnityEvent<float> onScroll = new();
        
        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Middle) return;

            onMiddleDrag.Invoke(eventData.delta);
        }

        public void OnScroll(PointerEventData eventData)
        {
            onScroll.Invoke(eventData.scrollDelta.y);
        }
    }
}