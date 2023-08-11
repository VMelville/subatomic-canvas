using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class VfxVisualizePresenter : IStartable
    {
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private VfxVisualizeView _vfxVisualizeView;
        [Inject] private TimeState _timeState;

        public void Start()
        {
            _lastSimulationCondition.Result
                .Select(resultTuple => resultTuple.Item1)
                .Where(result => result != null)
                .Select(result => result.LinearTrajectory)
                .Subscribe(_vfxVisualizeView.PlayVfx);
            _timeState.NowTime.Subscribe(_vfxVisualizeView.OnPassesTime);
        }
    }
}