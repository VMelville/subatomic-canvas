using UniRx;

namespace SubatomicCanvas.Model
{
    public class GlobalSettingState
    {
        public IReadOnlyReactiveProperty<bool> IsDisplayParticleName => _isDisplayParticleName;
        public IReadOnlyReactiveProperty<bool> IsDisplayLineVisualizer => _isDisplayLineVisualizer;

        private readonly BoolReactiveProperty _isDisplayParticleName = new(true);
        private readonly BoolReactiveProperty _isDisplayLineVisualizer = new(false);

        public void SetIsDisplayParticleName(bool isOn) => _isDisplayParticleName.Value = isOn;
        public void SetIsDisplayLineVisualizer(bool isOn) => _isDisplayLineVisualizer.Value = isOn;
    }
}