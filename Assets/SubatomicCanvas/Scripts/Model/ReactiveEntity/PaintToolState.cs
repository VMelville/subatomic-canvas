using UniRx;

namespace SubatomicCanvas.Model
{
    public class PaintToolState
    {
        // initialValue は仮で設定してあります。
        public readonly StringReactiveProperty activeDetectorKey = new("TrackDetectorV1");
        public readonly BoolReactiveProperty isActiveSymmetry = new(true);

        public const string ViewModeKey = "ViewMode";
    }
}