using System.Collections.Generic;
using UniRx;

namespace SubatomicCanvas.Model
{
    public class AvailableParticles
    {
        public IReadOnlyReactiveDictionary<string, Particle> ParticleDict => _particleDict;
        
        private readonly ReactiveDictionary<string, Particle> _particleDict = new();

        public void Add(string key, Particle particle) => _particleDict.Add(key, particle);

        public IEnumerable<Particle> GetParticles() => _particleDict.Values;
    }
}