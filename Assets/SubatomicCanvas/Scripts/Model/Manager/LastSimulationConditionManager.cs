using System.Collections.Generic;
using ParticleSim.Result;
using UniRx;

namespace SubatomicCanvas.Model
{
    public class LastSimulationConditionManager
    {
        public IReadOnlyReactiveProperty<(SimulationResult, Dictionary<string, (int, int)>)> Result => _result;
        public IReadOnlyReactiveProperty<string> ParticleKey => _particleKey;

        // ToDo: Resultの型がだいぶきもいので、いい感じにしたい
        private readonly ReactiveProperty<(SimulationResult, Dictionary<string, (int, int)>)> _result = new();
        private readonly ReactiveProperty<string> _particleKey = new("");

        public void SetResult(SimulationResult result, Dictionary<string, (int, int)> table, string particleKey)
        {
            _result.Value = (result, new Dictionary<string, (int, int)>(table));
            _particleKey.Value = particleKey;
        }
    }
}