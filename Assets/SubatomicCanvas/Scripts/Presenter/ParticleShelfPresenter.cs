using System.Collections.Generic;
using ParticleSim;
using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class ParticleShelfPresenter : IInitializable, IStartable
    {
        // Model
        [Inject] private AvailableParticles _availableParticles;
        [Inject] private CanvasState _canvasState;
        
        // View
        [Inject] private ParticleShelfView _particleShelfView;

        public void Initialize()
        {
            _particleShelfView.onValueChanged.AddListener(OnValueChanged);
        }
        
        public void Start()
        {
            Debug.LogWarning("ToDo: 一旦テストでPresenterに直接粒子リストを記載しているので適切な場所に書く");
            var testParticleList = new List<string> { "gamma", "e-", "mu-", "pi0", "pi+", "kaon0S", "Upsilon", "lambda_b" };

            foreach (var particle in testParticleList)
            {
                _particleShelfView.AddNewToggle(particle, (float)PDG.GetPDGMass(particle));
            }
        }

        private void OnValueChanged(string particle, bool isOn)
        {
            if (isOn)
            {
                _availableParticles.particleDict[particle] = new Particle();
            }
            else
            {
                _availableParticles.particleDict.Remove(particle);
            }
        }
    }
}