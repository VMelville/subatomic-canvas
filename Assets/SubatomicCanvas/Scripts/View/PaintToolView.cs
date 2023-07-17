using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SubatomicCanvas.View
{
    public class PaintToolView : MonoBehaviour
    {
        // ToDo: PaintTool系の入力を設置
        [SerializeField] private List<ButtonWithOverlayView> detectorButtons;
        [SerializeField] private SymmetryToolButtonView symmetryModeButton;

        public UnityEvent<string> onClickPaintToolButton;
        public UnityEvent onClickSymmetryModeButton => symmetryModeButton.onclick;

        private void Start()
        {
            foreach (var button in detectorButtons)
            {
                button.onClick.AddListener(() => onClickPaintToolButton.Invoke(button.GetDetectorKey()));
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