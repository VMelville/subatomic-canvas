using UniRx;

namespace SubatomicCanvas.Model
{
    public class PaintToolState
    {
        public ReactiveProperty<PaintToolType> activePaintToolType;
        public StringReactiveProperty activeDetectorKey;
        public BoolReactiveProperty isActiveSymmetry;
    }
}