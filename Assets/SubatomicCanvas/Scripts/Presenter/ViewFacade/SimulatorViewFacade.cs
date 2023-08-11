using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class SimulatorViewFacade : ControllerBase, IStartable
    {
        [Inject] private SimulatorView _view;

        [Inject] private AvailableParticlesManager _availableParticlesManager;
        [Inject] private LastSimulationConditionManager _lastSimulationConditionManager;
        [Inject] private GlobalSettingManager _globalSettingManager;

        public void Start()
        {
            _lastSimulationConditionManager.ParticleKey
                .Where(key => key!="")
                .Select(key=>_availableParticlesManager.ParticleDict[key].displayName)
                .Subscribe(_view.SetText)
                .AddTo(this);

            _globalSettingManager.IsDisplayParticleName
                .Subscribe(_view.SetDisplayParticleName)
                .AddTo(this);
        }
    }
}