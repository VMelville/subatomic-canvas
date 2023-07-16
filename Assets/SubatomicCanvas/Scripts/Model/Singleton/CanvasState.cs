using UniRx;

namespace SubatomicCanvas.Model
{
    public class CanvasState
    {
        public ReactiveProperty<CanvasData> canvasData = new ReactiveProperty<CanvasData>(new CanvasData());
    }
}