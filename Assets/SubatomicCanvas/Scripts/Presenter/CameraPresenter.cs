using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CameraPresenter : IStartable
    {
        // Model
        [Inject] private CameraManager _cameraManager;
        
        // View
        [Inject] private RaycastTargetView _raycastTargetView;
        [Inject] private CameraView _cameraView;
        [Inject] private ScreenView _screenView;
        [Inject] private CanvasView _canvasView;
        
        public void Start()
        {
            _cameraManager.Position.Subscribe(position =>
            {
                _cameraView.UpdateCamera(position, _cameraManager.ZoomLevel.Value);
                _screenView.UpdateScreen(position, _cameraManager.ZoomLevel.Value);
            });
            _cameraManager.ZoomLevel.Subscribe(zoomLevel =>
            {
                _cameraView.UpdateCamera(_cameraManager.Position.Value, zoomLevel);
                _screenView.UpdateScreen(_cameraManager.Position.Value, zoomLevel);
            });
            _cameraManager.Is2dView.Subscribe(_cameraView.SetOrthographic);
            
            _raycastTargetView.OnMiddleDrag.AddListener(_cameraManager.Grab);
            _raycastTargetView.OnScrollView.AddListener(_cameraManager.Zoom);
            
            _canvasView.OnAddCellView.AddListener((_, cellView) =>
            {
                cellView.OnMiddleDrag.AddListener(eventData => _cameraManager.Grab(eventData.delta));
                cellView.OnScroll.AddListener(eventData => _cameraManager.Zoom(eventData.scrollDelta.y));
            });
        }
    }
}