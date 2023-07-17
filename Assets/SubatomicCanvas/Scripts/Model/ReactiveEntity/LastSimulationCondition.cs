using ParticleSim.Result;
using UniRx;

namespace SubatomicCanvas.Model
{
    public class LastSimulationCondition
    {
        public readonly ReactiveProperty<SimulationResult> result = new();
        public readonly ReactiveProperty<CanvasData> canvasData = new();
    }
}