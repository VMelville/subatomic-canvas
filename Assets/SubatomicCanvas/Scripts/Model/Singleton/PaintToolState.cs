using UniRx;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    public class PaintToolState
    {
        public ReactiveProperty<PaintToolType> activePaintToolType = new ReactiveProperty<PaintToolType>();
        public StringReactiveProperty activeDetectorKey = new StringReactiveProperty();
        public BoolReactiveProperty isActiveSymmetry = new BoolReactiveProperty();

        public void ToggleSymmetryMode()
        {
            isActiveSymmetry.Value ^= true;
        }

        public void ChangePaintTool(string toolKey)
        {
            activeDetectorKey.Value = toolKey;

            Debug.LogWarning("ToDo: activePaintToolTypeの更新を行う");
        }
    }
}