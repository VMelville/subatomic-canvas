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
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private GlobalSettingState _globalSettingState;
        
        [Inject] private LineVisualizeView _lineVisualizeView;
        
        public void Start()
        {
            _lastSimulationCondition.Result
                .Select(resultTuple => resultTuple.Item1)
                .Where(result => result != null)
                .Subscribe(result =>
                {
                    _lineVisualizeView.ClearLine();
                    _lineVisualizeView.DrawLine(result.Trajectories);
                });
            _globalSettingState.IsDisplayLineVisualizer.Subscribe(_lineVisualizeView.gameObject.SetActive);
        }
    }
}