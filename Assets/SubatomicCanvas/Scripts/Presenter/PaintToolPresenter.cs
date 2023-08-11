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
        [Inject] private PaintToolManager _paintToolManager;
        [Inject] private AvailableDetectors _availableDetectors;
        
        // View
        [Inject] private PaintToolView _paintToolView;
        
        public void Start()
        {
            _paintToolManager.ActiveDetectorKey.Subscribe(_paintToolView.SetDetectorKey);
            _paintToolManager.IsActiveSymmetry.Subscribe(_paintToolView.SetActiveSymmetry);
            
            _paintToolView.OnClickPaintToolButton.AddListener(_paintToolManager.SetActiveDetectorKey);
            _paintToolView.OnClickSymmetryModeButton.AddListener(_paintToolManager.ToggleActiveSymmetry);
        }
    }
}