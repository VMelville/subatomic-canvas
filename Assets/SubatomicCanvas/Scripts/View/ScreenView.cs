using UnityEngine;

namespace SubatomicCanvas.View
{
    public class ScreenView : MonoBehaviour
    {
        public void UpdateScreen(Vector2 position, float zoomLevel)
        {
            var rt = (RectTransform)transform;
            rt.anchoredPosition = 1000f * zoomLevel * position;
            rt.localScale = zoomLevel * Vector3.one;
        }
    }
}