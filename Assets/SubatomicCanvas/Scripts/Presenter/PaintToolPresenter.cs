using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class PaintToolPresenter : IStartable
    {
        // Model
        [Inject] private PaintToolState _paintToolState;
        [Inject] private AvailableDetectors _availableDetectors;
        
        // View
        [Inject] private PaintToolView _paintToolView;
        
        public void Start()
        {
            _paintToolState.ActiveDetectorKey.Subscribe(_paintToolView.SetDetectorKey);
            _paintToolState.IsActiveSymmetry.Subscribe(_paintToolView.SetActiveSymmetry);
            
            _paintToolView.OnClickPaintToolButton.AddListener(_paintToolState.SetActiveDetectorKey);
            _paintToolView.OnClickSymmetryModeButton.AddListener(_paintToolState.ToggleActiveSymmetry);
        }
    }
}