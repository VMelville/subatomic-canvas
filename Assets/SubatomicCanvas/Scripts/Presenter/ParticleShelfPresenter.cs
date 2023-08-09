using System.Collections.Generic;
using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
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
            _particleShelfView.OnValueChanged.AddListener(OnValueChanged);
        }
        
        public void Start()
        {
            foreach (var particle in _availableParticles.particleDict.Values)
            {
                _particleShelfView.AddNewToggle(particle);
            }

            _canvasState.usingParticleKeys.ObserveAdd()
                .Subscribe(addEvent => _particleShelfView.SetIsOn(addEvent.Value, true));
            
            _canvasState.usingParticleKeys.ObserveRemove()
                .Subscribe(removeEvent => _particleShelfView.SetIsOn(removeEvent.Value, false));

            _canvasState.usingParticleKeys.ObserveReset()
                .Subscribe(_ => _particleShelfView.SetOnParticles(new List<string>(_canvasState.usingParticleKeys)));
        }

        private void OnValueChanged(string particle, bool isOn)
        {
            if (isOn)
            {
                if (!_canvasState.usingParticleKeys.Contains(particle))
                {
                    _canvasState.usingParticleKeys.Add(particle);
                }
            }
            else
            {
                _canvasState.usingParticleKeys.Remove(particle);
            }
        }
    }
}