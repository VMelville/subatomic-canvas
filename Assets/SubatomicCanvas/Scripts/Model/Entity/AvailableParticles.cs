using System.Collections.Generic;

namespace SubatomicCanvas.Model
{
    public class AvailableParticles
    {
        public IReadOnlyDictionary<string, Particle> ParticleDict => _particleDict;
        
        private readonly Dictionary<string, Particle> _particleDict = new();

        public void Add(string key, Particle particle) => _particleDict.Add(key, particle);

        public IEnumerable<Particle> GetParticles() => _particleDict.Values;
    }
}