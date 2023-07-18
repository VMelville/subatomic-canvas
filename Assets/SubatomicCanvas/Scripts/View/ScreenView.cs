using UnityEngine;

namespace SubatomicCanvas.View
{
    public class ScreenView : MonoBehaviour
    {
        public void SetAnchoredPosition(Vector2 anchoredPosition)
        {
            ((RectTransform)transform).anchoredPosition = anchoredPosition;
        }

        public void SetLocalScale(Vector3 localScale)
        {
            ((RectTransform)transform).localScale = localScale;
        }
    }
}