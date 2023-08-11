using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CameraManagerFacade : IStartable
    {
        [Inject] private CameraManager _manager;
        
        [Inject] private RaycastTargetView _raycastTargetView;
        [Inject] private CanvasView _canvasView;

        public void Start()
        {
            _raycastTargetView.OnMiddleDrag.AddListener(_manager.Grab);
            _raycastTargetView.OnScrollView.AddListener(_manager.Zoom);
            
            _canvasView.OnAddCellView.AddListener((_, cellView) =>
            {
                cellView.OnMiddleDrag.AddListener(eventData => _manager.Grab(eventData.delta));
                cellView.OnScroll.AddListener(eventData => _manager.Zoom(eventData.scrollDelta.y));
            });
        }
    }
}