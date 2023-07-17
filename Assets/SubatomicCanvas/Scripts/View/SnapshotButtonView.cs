using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SnapshotButtonView : MonoBehaviour
    {
        [SerializeField] private Button captureButton;

        public UnityEvent onClick => captureButton.onClick;
    }
}
