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
        [Inject] private CameraState _cameraState;
        
        // View
        [Inject] private RaycastTargetView _raycastTargetView;
        [Inject] private CameraView _cameraView;
        [Inject] private ScreenView _screenView;
        [Inject] private CanvasView _canvasView;
        
        public void Start()
        {
            _cameraState.Position.Subscribe(position =>
            {
                _cameraView.UpdateCamera(position, _cameraState.ZoomLevel.Value);
                _screenView.UpdateScreen(position, _cameraState.ZoomLevel.Value);
            });
            _cameraState.ZoomLevel.Subscribe(zoomLevel =>
            {
                _cameraView.UpdateCamera(_cameraState.Position.Value, zoomLevel);
                _screenView.UpdateScreen(_cameraState.Position.Value, zoomLevel);
            });
            _cameraState.Is2dView.Subscribe(_cameraView.SetOrthographic);
            
            _raycastTargetView.OnMiddleDrag.AddListener(_cameraState.Grab);
            _raycastTargetView.OnScrollView.AddListener(_cameraState.Zoom);
            
            _canvasView.OnAddCellView.AddListener((_, cellView) =>
            {
                cellView.OnMiddleDrag.AddListener(eventData => _cameraState.Grab(eventData.delta));
                cellView.OnScroll.AddListener(eventData => _cameraState.Zoom(eventData.scrollDelta.y));
            });
        }
    }
}