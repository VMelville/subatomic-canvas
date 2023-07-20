using ParticleSim.Result;
using UniRx;

namespace SubatomicCanvas.Model
{
    public class LastSimulationCondition
    {
        public readonly ReactiveProperty<SimulationResult> result = new();
        public readonly ReactiveProperty<string> particleKey = new();
        public ReactiveDictionary<(int, int), string> detectorKeyDict = new();
    }
}