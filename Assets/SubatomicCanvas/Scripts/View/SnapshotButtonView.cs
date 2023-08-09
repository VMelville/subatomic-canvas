using UnityEngine;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class SnapshotButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;

        public Button.ButtonClickedEvent OnClick => button.onClick;
    }
}
