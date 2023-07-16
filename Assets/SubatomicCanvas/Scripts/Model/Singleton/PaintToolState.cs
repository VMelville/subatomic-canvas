using UniRx;

namespace SubatomicCanvas.Model
{
    public class PaintToolState
    {
        public ReactiveProperty<PaintToolType> activePaintToolType = new ReactiveProperty<PaintToolType>();
        public StringReactiveProperty activeDetectorKey = new StringReactiveProperty();
        public BoolReactiveProperty isActiveSymmetry = new BoolReactiveProperty();
    }
}