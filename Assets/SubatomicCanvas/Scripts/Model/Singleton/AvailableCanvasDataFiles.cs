using UniRx;

namespace SubatomicCanvas.Model
{
    public class AvailableCanvasDataFiles
    {
        public readonly ReactiveCollection<CanvasDataFileInfo> canvasDataFiles = new ReactiveCollection<CanvasDataFileInfo>();
    }
}