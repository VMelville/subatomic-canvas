using UniRx;

namespace SubatomicCanvas.Model
{
    public class MenuState
    {
        public IntReactiveProperty pageIndex = new IntReactiveProperty();
        public BoolReactiveProperty isOpen = new BoolReactiveProperty();
    }
}