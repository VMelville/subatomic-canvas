using System.Collections.Generic;
using ParticleSim.Result;
using UniRx;

namespace SubatomicCanvas.Model
{
    public class LastSimulationCondition
    {
        public IReadOnlyReactiveProperty<(SimulationResult, Dictionary<string, (int, int)>)> Result => _result;
        public IReadOnlyReactiveProperty<string> ParticleKey => _particleKey;
        public IReadOnlyReactiveDictionary<(int, int), string> DetectorKeyDict => _detectorKeyDict;

        // ToDo: Resultの型がだいぶきもいので、いい感じにしたい
        private readonly ReactiveProperty<(SimulationResult, Dictionary<string, (int, int)>)> _result = new();
        private readonly ReactiveProperty<string> _particleKey = new();
        private readonly ReactiveDictionary<(int, int), string> _detectorKeyDict = new();

        public void SetResult(SimulationResult result, Dictionary<string, (int, int)> table) => _result.Value = (result, new Dictionary<string, (int, int)>(table));
        public void SetParticleKey(string particleKey) => _particleKey.Value = particleKey;

        public void SetDetectorKeyDict(IReadOnlyReactiveDictionary<(int, int), string> detectorKeyDict)
        {
            _detectorKeyDict.Clear();
            
            foreach (var (position, detector) in detectorKeyDict)
            {
                _detectorKeyDict.Add(position,detector);
            }
        }
    }
}