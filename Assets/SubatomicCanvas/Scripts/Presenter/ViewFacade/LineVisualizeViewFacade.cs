using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class LineVisualizeViewFacade : ControllerBase, IInitializable
    {
        [Inject] private LineVisualizeView _view;
        
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private GlobalSettingManager _globalSettingManager;
        
        public void Initialize()
        {
            _lastSimulationConditionManager.Result
                .Select(resultTuple => resultTuple.Item1)
                .Where(result => result != null)
                .Subscribe(result =>
                {
                    _view.ClearLine();
                    _view.DrawLine(result.Trajectories);
                })
                .AddTo(this);
            
            _globalSettingManager.IsDisplayLineVisualizer
                .Subscribe(_view.gameObject.SetActive)
                .AddTo(this);
        }
    }
}