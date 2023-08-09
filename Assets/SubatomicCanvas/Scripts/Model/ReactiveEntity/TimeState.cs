using UniRx;

namespace SubatomicCanvas.Model
{
    public class TimeState
    {
        public readonly FloatReactiveProperty time = new();
        public readonly FloatReactiveProperty speed = new(0f);
    }
}