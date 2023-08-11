using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class VfxVisualizePresenter : IStartable
    {
        // Model
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private TimeManager _timeManager;
        
        // View
        [Inject] private VfxVisualizeView _vfxVisualizeView;

        public void Start()
        {
            _lastSimulationConditionManager.Result
                .Select(resultTuple => resultTuple.Item1)
                .Where(result => result != null)
                .Select(result => result.LinearTrajectory)
                .Subscribe(_vfxVisualizeView.PlayVfx);
            
            _timeManager.NowTime.Subscribe(_vfxVisualizeView.OnPassesTime);
        }
    }
}