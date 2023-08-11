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
            _particleShelfView.OnValueChanged.AddListener(_canvasState.SetParticleState);
        }
        
        public void Start()
        {
            foreach (var particle in _availableParticles.GetParticles())
            {
                _particleShelfView.AddNewToggle(particle);
            }

            _canvasState.UsingParticleKeys.ObserveAdd()
                .Subscribe(addEvent => _particleShelfView.SetIsOn(addEvent.Value, true));
            
            _canvasState.UsingParticleKeys.ObserveRemove()
                .Subscribe(removeEvent => _particleShelfView.SetIsOn(removeEvent.Value, false));

            _canvasState.UsingParticleKeys.ObserveReset()
                .Subscribe(_ => _particleShelfView.SetOnParticles(_canvasState.GetUsingParticleKeys()));
        }
    }
}