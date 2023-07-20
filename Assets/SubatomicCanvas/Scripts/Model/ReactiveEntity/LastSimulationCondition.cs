using System.Collections.Generic;
using ParticleSim.Result;
using UniRx;

namespace SubatomicCanvas.Model
{
    public class LastSimulationCondition
    {
        // ToDo: Resultの型がだいぶきもいので、いい感じにしたい
        public readonly ReactiveProperty<(SimulationResult, Dictionary<string, (int, int)>)> result = new();
        public readonly ReactiveProperty<string> particleKey = new();
        public ReactiveDictionary<(int, int), string> detectorKeyDict = new();
    }
}