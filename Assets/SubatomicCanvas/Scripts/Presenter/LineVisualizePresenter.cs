using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class LineVisualizePresenter : IStartable
    {
        // Model
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private GlobalSettingManager _globalSettingManager;
        
        [Inject] private LineVisualizeView _lineVisualizeView;
        
        public void Start()
        {
            _lastSimulationConditionManager.Result
                .Select(resultTuple => resultTuple.Item1)
                .Where(result => result != null)
                .Subscribe(result =>
                {
                    _lineVisualizeView.ClearLine();
                    _lineVisualizeView.DrawLine(result.Trajectories);
                });
            _globalSettingManager.IsDisplayLineVisualizer.Subscribe(_lineVisualizeView.gameObject.SetActive);
        }
    }
}