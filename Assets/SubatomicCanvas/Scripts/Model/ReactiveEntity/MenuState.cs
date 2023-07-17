using UniRx;

namespace SubatomicCanvas.Model
{
    public class MenuState
    {
        public IntReactiveProperty pageIndex = new();
        public BoolReactiveProperty isOpen = new();
    }
}