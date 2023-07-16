using UniRx;

namespace SubatomicCanvas.Model
{
    public class TimeState
    {
        public FloatReactiveProperty time = new FloatReactiveProperty();
        public FloatReactiveProperty speed = new FloatReactiveProperty();
    }
}