using UniRx;

namespace SubatomicCanvas.Model
{
    public class ModeState
    {
        public ReactiveProperty<ModeType> modeType = new();
    }
}