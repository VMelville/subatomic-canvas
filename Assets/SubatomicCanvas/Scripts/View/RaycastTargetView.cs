using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SubatomicCanvas.View
{
    public class RaycastTargetView : MonoBehaviour, IDragHandler, IScrollHandler
    {
        public UnityEvent<Vector2> OnMiddleDrag = new();
        public UnityEvent<float> OnScrollView = new();
        
        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Middle) return;

            OnMiddleDrag.Invoke(eventData.delta);
        }

        public void OnScroll(PointerEventData eventData)
        {
            OnScrollView.Invoke(eventData.scrollDelta.y);
        }
    }
}