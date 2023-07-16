using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class CanvasPresenter : IStartable
    {
        [Inject] private CanvasState _canvasState;
        [Inject] private CanvasView _canvasView;

        public void Start()
        {
            _canvasState.canvasData.Subscribe(OnChangeCanvasState);
        }

        private void OnChangeCanvasState(CanvasData data)
        {
            _canvasView.ReloadCanvas();
        }
    }
}