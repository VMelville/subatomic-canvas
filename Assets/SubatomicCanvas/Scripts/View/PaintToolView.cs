using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SubatomicCanvas.View
{
    public class PaintToolView : MonoBehaviour
    {
        // ToDo: PaintTool系の入力を設置
        [SerializeField] private List<ButtonWithOverlayView> detectorButtons;
        [SerializeField] private SymmetryToolButtonView symmetryModeButton;

        public UnityEvent<string> OnClickPaintToolButton;
        public Button.ButtonClickedEvent OnClickSymmetryModeButton => symmetryModeButton.Onclick;

        private void Start()
        {
            foreach (var button in detectorButtons)
            {
                button.OnClick.AddListener(() => OnClickPaintToolButton.Invoke(button.GetDetectorKey()));
            }
        }

        public void SetDetectorKey(string key)
        {
            foreach (var button in detectorButtons)
            {
                button.SetActiveOverlay(key != button.GetDetectorKey());
            }
        }

        public void SetActiveSymmetry(bool isSymmetry)
        {
            symmetryModeButton.SwitchSymmetryIcon(isSymmetry);
        }
    }
}