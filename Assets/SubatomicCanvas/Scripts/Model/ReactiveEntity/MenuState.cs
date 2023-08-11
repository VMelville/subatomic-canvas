using UniRx;

namespace SubatomicCanvas.Model
{
    public class MenuState
    {
        public IReadOnlyReactiveProperty<int> PageIndex => _pageIndex;
        public IReadOnlyReactiveProperty<bool> IsOpen => _isOpen;
        public IReadOnlyReactiveProperty<float> EasingDuration => _easingDuration;

        private readonly IntReactiveProperty _pageIndex = new();
        private readonly BoolReactiveProperty _isOpen = new();
        private readonly FloatReactiveProperty _easingDuration = new(0.0f);

        public void SetPageIndex(int pageIndex) => _pageIndex.Value = pageIndex;
        public void ToggleOpen() => _isOpen.Value ^= true;
        public void SetDuration(float duration) => _easingDuration.Value = duration;
    }
}