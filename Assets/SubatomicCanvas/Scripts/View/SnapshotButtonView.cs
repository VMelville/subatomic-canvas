using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SnapshotButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;

        public UnityEvent onClick => button.onClick;
    }
}
