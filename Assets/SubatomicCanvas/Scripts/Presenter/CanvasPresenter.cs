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
        [Inject] private CanvasState _canvasState;
        [Inject] private PaintToolState _paintToolState;
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private TimeState _timeState;

        // View
        [Inject] private CanvasView _canvasView;

        public void Start()
        {
            _canvasState.CanvasSize.Subscribe(canvasSize => _canvasView.BuildCanvas(canvasSize, _canvasState.CellSize.Value));
            _canvasState.CellSize.Subscribe(cellSize=> _canvasView.BuildCanvas(_canvasState.CanvasSize.Value, cellSize));
            _canvasState.InstalledDetectorPositionAndKeys.ObserveAdd()
                .Subscribe(addEvent => _canvasView.PutDetector(addEvent.Key, addEvent.Value, _canvasState.CellSize.Value));
            _canvasState.InstalledDetectorPositionAndKeys.ObserveReplace()
                .Subscribe(replaceEvent =>
                {
                    _canvasView.RemoveDetector(replaceEvent.Key);
                    _canvasView.PutDetector(replaceEvent.Key, replaceEvent.NewValue, _canvasState.CellSize.Value);
                });
            _canvasState.InstalledDetectorPositionAndKeys.ObserveRemove()
                .Subscribe(removeEvent => _canvasView.RemoveDetector(removeEvent.Key));
            _canvasState.InstalledDetectorPositionAndKeys.ObserveReset()
                .Subscribe(_ => _canvasView.RemoveDetectorAll());
            _lastSimulationCondition.Result.Subscribe(_canvasView.ApplySimulationResult);
            _timeState.NowTime.Subscribe(_canvasView.SeekTime);
            
            // ToDo: ここは結構複雑なのでViewに押し付けたい
            _canvasView.OnAddCellView.AddListener((position, view) =>
            {
                view.OnBeDrawed.AddListener(() =>
                {
                    if (_paintToolState.ActiveDetectorKey.Value == PaintToolState.ViewModeKey) return;
                    _canvasState.PutDetector(position, _paintToolState.ActiveDetectorKey.Value, _paintToolState.IsActiveSymmetry.Value);
                });
                
                view.OnBeElased.AddListener(() =>
                {
                    if (_paintToolState.ActiveDetectorKey.Value == PaintToolState.ViewModeKey) return;
                    _canvasState.RemoveDetector(position, _paintToolState.IsActiveSymmetry.Value);
                });
            });
            
            _canvasState.SetCanvasSize(3);
        }
    }
}