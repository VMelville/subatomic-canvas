using UniRx;

namespace SubatomicCanvas.Model
{
    public class MenuState
    {
        public readonly IntReactiveProperty pageIndex = new();
        public readonly BoolReactiveProperty isOpen = new();
    }
}