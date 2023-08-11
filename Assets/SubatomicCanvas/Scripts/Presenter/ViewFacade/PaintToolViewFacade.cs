using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class PaintToolViewFacade : IInitializable
    {
        [Inject] private PaintToolView _view;
        
        [Inject] private PaintToolManager _paintToolManager;
        
        public void Initialize()
        {
            _paintToolManager.ActiveDetectorKey.Subscribe(_view.SetDetectorKey);
            _paintToolManager.IsActiveSymmetry.Subscribe(_view.SetActiveSymmetry);
        }
    }
}