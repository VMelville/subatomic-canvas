using UniRx;

namespace SubatomicCanvas.Model
{
    public class SaveLoadState
    {
        public readonly ReactiveCollection<(string, CanvasDataFileInfo)> canvasDataFiles = new();
        public readonly StringReactiveProperty fileNameCandidate = new();
        public readonly BoolReactiveProperty isDisplayTrashButton = new();
    }
}