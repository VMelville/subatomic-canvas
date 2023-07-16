using UniRx;

namespace SubatomicCanvas.Model
{
    public class CanvasData
    {
        public ReactiveCollection<string> usingParticleKeys;
        public ReactiveCollection<((int, int), string)> installedDetectorPositionAndKeys;
        public Vector3ReactiveProperty magneticFieldVector;
        public FloatReactiveProperty cellSize;
        public IntReactiveProperty canvasSize;
    }
}