using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class VfxVisualizeViewFacade : IStartable
    {
        [Inject] private VfxVisualizeView _view;
        
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private TimeManager _timeManager;
        
        public void Start()
        {
            _lastSimulationConditionManager.Result
                .Select(resultTuple => resultTuple.Item1)
                .Where(result => result != null)
                .Select(result => result.LinearTrajectory)
                .Subscribe(_view.PlayVfx);
            
            _timeManager.NowTime.Subscribe(_view.OnPassesTime);
        }
    }
}