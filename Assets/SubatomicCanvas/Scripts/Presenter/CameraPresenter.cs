using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
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
            _cameraState.position.Subscribe(OnChangePosition);
            _cameraState.zoomLevel.Subscribe(OnChangeZoomLevel);
            _cameraState.is2dView.Subscribe(_cameraView.SetOrthographic);
            
            _raycastTargetView.onMiddleDrag.AddListener(OnGrab);
            _raycastTargetView.onScroll.AddListener(OnZoom);
            
            _canvasView.onAddCellView.AddListener(ListenCellEvent);
        }

        private void OnChangePosition(Vector2 _) => UpdateCameraView();
        
        private void OnChangeZoomLevel(float _) => UpdateCameraView();
        
        private void OnCellMiddleDrag((int, int) _, PointerEventData eventData) => OnGrab(eventData.delta);

        private void OnCellScroll((int, int) _, PointerEventData eventData) => OnZoom(eventData.scrollDelta.y);

        private void UpdateCameraView()
        {
            _cameraView.SetPosition(-_cameraState.position.Value);
            _cameraView.SetRotation(new Quaternion()); // 一旦 Rotation はいじらない
            _cameraView.SetOrthographic(_cameraState.is2dView.Value);
            _cameraView.SetOrthographicSize(Screen.height / 2f / _cameraState.zoomLevel.Value * 0.001f);

            _screenView.SetAnchoredPosition(1000f * _cameraState.zoomLevel.Value * _cameraState.position.Value);
            _screenView.SetLocalScale(_cameraState.zoomLevel.Value * Vector3.one);
        }
        
        private void ListenCellEvent((int, int) position, CellView cellView)
        {
            cellView.onMiddleDrag.AddListener(eventData => OnCellMiddleDrag(position, eventData));
            cellView.onScroll.AddListener(eventData => OnCellScroll(position, eventData));
        }

        private void OnGrab(Vector2 delta)
        {
            _cameraState.position.Value += delta * 0.001f / _cameraState.zoomLevel.Value;
        }
        
        private void OnZoom(float zoom)
        {
            _cameraState.zoomLevel.Value *= 1f + zoom * 0.1f;
        }

        private void OnChangeCanvasState(CanvasData data)
        {
            _canvasView.ClearCanvas();
            _canvasView.ReloadCanvas(data.canvasSize);
        }
    }
}