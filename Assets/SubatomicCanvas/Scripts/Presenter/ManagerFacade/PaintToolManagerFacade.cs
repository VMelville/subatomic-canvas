using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class PaintToolManagerFacade : IStartable
    {
        [Inject] private PaintToolManager _manager;
        
        [Inject] private PaintToolView _paintToolView;
        
        public void Start()
        {
            _paintToolView.OnClickPaintToolButton.AddListener(_manager.SetActiveDetectorKey);
            _paintToolView.OnClickSymmetryModeButton.AddListener(_manager.ToggleActiveSymmetry);
        }
    }
}