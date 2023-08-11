using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CanvasViewFacade : ControllerBase, IInitializable
    {
        [Inject] private CanvasView _view;
        
        [Inject] private CanvasManager _canvasManager;
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private TimeManager _timeManager;

        public void Initialize()
        {
            _canvasManager.CanvasSize
                .Subscribe(canvasSize => _view.BuildCanvas(canvasSize, _canvasManager.CellSize.Value, _canvasManager.DetectorPlacements))
                .AddTo(this);
            
            _canvasManager.CellSize
                .Subscribe(cellSize=> _view.BuildCanvas(_canvasManager.CanvasSize.Value, cellSize, _canvasManager.DetectorPlacements))
                .AddTo(this);
            
            _canvasManager.DetectorPlacements.ObserveAdd()
                .Subscribe(addEvent => _view.PutDetector(addEvent.Key, addEvent.Value, _canvasManager.CellSize.Value))
                .AddTo(this);
            
            _canvasManager.DetectorPlacements.ObserveReplace()
                .Subscribe(replaceEvent =>
                {
                    _view.RemoveDetector(replaceEvent.Key);
                    _view.PutDetector(replaceEvent.Key, replaceEvent.NewValue, _canvasManager.CellSize.Value);
                })
                .AddTo(this);
            
            _canvasManager.DetectorPlacements.ObserveRemove()
                .Subscribe(removeEvent => _view.RemoveDetector(removeEvent.Key))
                .AddTo(this);
            
            _canvasManager.DetectorPlacements.ObserveReset()
                .Subscribe(_ => _view.RemoveDetectorAll())
                .AddTo(this);
            
            _lastSimulationConditionManager.Result
                .Subscribe(_view.ApplySimulationResult)
                .AddTo(this);
            
            _timeManager.NowTime
                .Subscribe(_view.SeekTime)
                .AddTo(this);
        }
    }
}