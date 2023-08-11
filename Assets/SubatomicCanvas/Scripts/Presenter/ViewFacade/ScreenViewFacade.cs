using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class ScreenViewFacade : ControllerBase, IStartable
    {
        [Inject] private ScreenView _view;
        
        [Inject] private CameraManager _cameraManager;

        public void Start()
        {
            _cameraManager.Position
                .Subscribe(position => _view.UpdateScreen(position, _cameraManager.ZoomLevel.Value))
                .AddTo(this);
            
            _cameraManager.ZoomLevel
                .Subscribe(zoomLevel => _view.UpdateScreen(_cameraManager.Position.Value, zoomLevel))
                .AddTo(this);
        }
    }
}