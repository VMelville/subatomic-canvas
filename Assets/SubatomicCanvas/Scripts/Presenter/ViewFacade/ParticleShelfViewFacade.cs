using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class ParticleShelfViewFacade : ControllerBase, IStartable
    {
        [Inject] private ParticleShelfView _view;
        
        [Inject] private AvailableParticlesManager _availableParticlesManager;
        [Inject] private CanvasManager _canvasManager;
        
        public void Start()
        {
            foreach (var particle in _availableParticlesManager.GetParticles())
            {
                _view.AddNewToggle(particle.pdgName, particle.displayName);
            }

            _canvasManager.UsingParticleKeys.ObserveAdd()
                .Subscribe(addEvent => _view.SetIsOn(addEvent.Value, true))
                .AddTo(this);
            
            _canvasManager.UsingParticleKeys.ObserveRemove()
                .Subscribe(removeEvent => _view.SetIsOn(removeEvent.Value, false))
                .AddTo(this);
            
            _canvasManager.UsingParticleKeys.ObserveReset()
                .Subscribe(_ => _view.SetOnParticles(_canvasManager.GetUsingParticleKeys()))
                .AddTo(this);
        }
    }
}