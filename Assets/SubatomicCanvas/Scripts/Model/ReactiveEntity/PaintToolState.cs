using UniRx;

namespace SubatomicCanvas.Model
{
    public class PaintToolState
    {
        // initialValue は仮で設定してあります。
        public ReactiveProperty<PaintToolType> activePaintToolType = new();
        public StringReactiveProperty activeDetectorKey = new("TrackDetectorV1");
        public BoolReactiveProperty isActiveSymmetry = new(true);

        public const string ViewModeKey = "ViewMode";
    }
}