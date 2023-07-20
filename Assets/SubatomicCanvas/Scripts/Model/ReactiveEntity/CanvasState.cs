using System.Numerics;
using UniRx;

namespace SubatomicCanvas.Model
{
    public class CanvasState
    {
        public ReactiveCollection<string> usingParticleKeys = new();
        public ReactiveDictionary<(int, int), string> installedDetectorPositionAndKeys = new();
        public ReactiveProperty<Vector3> magneticFieldVector = new();
        public ReactiveProperty<float> cellSize = new(0.1f);
        public ReactiveProperty<int> canvasSize = new(0);
    }
}