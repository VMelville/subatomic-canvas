using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CameraViewFacade : ControllerBase, IInitializable
    {
        [Inject] private CameraView _view;
        
        [Inject] private CameraManager _cameraManager;
        
        public void Initialize()
        {
            _cameraManager.Position
                .Subscribe(position => _view.UpdateCamera(position, _cameraManager.ZoomLevel.Value))
                .AddTo(this);
            
            _cameraManager.ZoomLevel
                .Subscribe(zoomLevel => _view.UpdateCamera(_cameraManager.Position.Value, zoomLevel))
                .AddTo(this);
            
            _cameraManager.Is2dView
                .Subscribe(_view.SetOrthographic)
                .AddTo(this);
        }
    }
}