using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class ButtonWithOverlayView : MonoBehaviour
    {
        [SerializeField] private string detectorKey;
        [SerializeField] private Button button;
        [SerializeField] private GameObject overlay;
    
        public UnityEvent onClick => button.onClick;

        public string GetDetectorKey()
        {
            return detectorKey;
        }
    
        public void SetActiveOverlay(bool isActive)
        {
            overlay.SetActive(isActive);
        }
    }
}