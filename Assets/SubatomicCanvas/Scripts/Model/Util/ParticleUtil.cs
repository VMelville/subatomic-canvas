using System.Linq;
using ParticleSim;
using UniRx;
using UnityEngine;

namespace SubatomicCanvas.Model
{
    public static class ParticleUtil
    {
        public static string GetPickedUpParticleKey(IReadOnlyReactiveCollection<string> usingParticleKeys)
        {
            var particleCount = usingParticleKeys.Count;
            return particleCount == 0 ? "" : usingParticleKeys.ElementAt(Random.Range(0, particleCount));
        }

        public static float GetPickedUpEnergy(float energyMin, float energyMax)
        {
            return Random.Range(energyMin, energyMax);
        }

        public static ParticleGun MakeParticleGun(string particleKey, float energy)
        {
            return new ParticleGun(particleKey, energy);
        }
    }
}