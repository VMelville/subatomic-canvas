using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CanvasPresenter : IStartable
    {
        // Model
        [Inject] private CanvasManager _canvasManager;
        [Inject] private PaintToolManager _paintToolManager;
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private TimeManager _timeManager;

        // View
        [Inject] private CanvasView _canvasView;

        public void Start()
        {
            _canvasManager.CanvasSize.Subscribe(canvasSize => _canvasView.BuildCanvas(canvasSize, _canvasManager.CellSize.Value));
            _canvasManager.CellSize.Subscribe(cellSize=> _canvasView.BuildCanvas(_canvasManager.CanvasSize.Value, cellSize));
            _canvasManager.InstalledDetectorPositionAndKeys.ObserveAdd()
                .Subscribe(addEvent => _canvasView.PutDetector(addEvent.Key, addEvent.Value, _canvasManager.CellSize.Value));
            _canvasManager.InstalledDetectorPositionAndKeys.ObserveReplace()
                .Subscribe(replaceEvent =>
                {
                    _canvasView.RemoveDetector(replaceEvent.Key);
                    _canvasView.PutDetector(replaceEvent.Key, replaceEvent.NewValue, _canvasManager.CellSize.Value);
                });
            _canvasManager.InstalledDetectorPositionAndKeys.ObserveRemove()
                .Subscribe(removeEvent => _canvasView.RemoveDetector(removeEvent.Key));
            _canvasManager.InstalledDetectorPositionAndKeys.ObserveReset()
                .Subscribe(_ => _canvasView.RemoveDetectorAll());
            _lastSimulationConditionManager.Result.Subscribe(_canvasView.ApplySimulationResult);
            _timeManager.NowTime.Subscribe(_canvasView.SeekTime);
            
            // ToDo: ここは結構複雑なのでViewに押し付けたい
            _canvasView.OnAddCellView.AddListener((position, view) =>
            {
                view.OnBeDrawed.AddListener(() =>
                {
                    if (_paintToolManager.ActiveDetectorKey.Value == PaintToolManager.ViewModeKey) return;
                    _canvasManager.PutDetector(position, _paintToolManager.ActiveDetectorKey.Value, _paintToolManager.IsActiveSymmetry.Value);
                });
                
                view.OnBeElased.AddListener(() =>
                {
                    if (_paintToolManager.ActiveDetectorKey.Value == PaintToolManager.ViewModeKey) return;
                    _canvasManager.RemoveDetector(position, _paintToolManager.IsActiveSymmetry.Value);
                });
            });
            
            _canvasManager.SetCanvasSize(3);
        }
    }
}