using UniRx;

namespace SubatomicCanvas.Model
{
    public class AvailableParticles
    {
        public readonly ReactiveDictionary<string, Particle> particleDict = new();
    }
}