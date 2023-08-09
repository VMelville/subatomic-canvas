using UniRx;

namespace SubatomicCanvas.Model
{
    public class GlobalSettingState
    {
        public readonly BoolReactiveProperty isDisplayParticleName = new(true);
        public readonly BoolReactiveProperty isDisplayLineVisualizer = new(false);
    }
}