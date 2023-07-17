using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class PaintToolPresenter : IStartable
    {
        [Inject] private PaintToolState _paintToolState;
        [Inject] private PaintToolView _paintToolView;
        [Inject] private AvailableDetectors _availableDetectors;
        
        public void Start()
        {
            _paintToolState.activeDetectorKey.Subscribe(_paintToolView.SetDetectorKey);
            _paintToolState.isActiveSymmetry.Subscribe(_paintToolView.SetActiveSymmetry);
            
            _paintToolView.onClickPaintToolButton.AddListener(_paintToolState.ChangePaintTool);
            _paintToolView.onClickSymmetryModeButton.AddListener(_paintToolState.ToggleSymmetryMode);
            
            // ToDo: これは仮です
            _paintToolState.ChangePaintTool("TrackDetectorV1");
            _paintToolState.ToggleSymmetryMode();
        }
    }
}