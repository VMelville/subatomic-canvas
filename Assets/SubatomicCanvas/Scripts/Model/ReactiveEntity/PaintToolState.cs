using UniRx;

namespace SubatomicCanvas.Model
{
    public class PaintToolState
    {
        // initialValue は仮で設定してあります。
        public ReactiveProperty<PaintToolType> activePaintToolType = new();
        public StringReactiveProperty activeDetectorKey = new("TrackValueV1");
        public BoolReactiveProperty isActiveSymmetry = new(true);
    }
}