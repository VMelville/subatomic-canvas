using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CanvasPresenter : IStartable
    {
        // Model
        [Inject] private CanvasState _canvasState;
        
        // View
        [Inject] private CanvasView _canvasView;

        public void Start()
        {
            _canvasState.canvasData.Subscribe(OnChangeCanvasState);
            
            Debug.LogWarning("ToDo: 配置を行えるようにする。");
        }

        private void OnChangeCanvasState(CanvasData data)
        {
            _canvasView.ClearCanvas();
            _canvasView.ReloadCanvas(data.canvasSize);
        }
    }
}