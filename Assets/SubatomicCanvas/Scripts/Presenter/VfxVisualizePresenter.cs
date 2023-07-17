using ParticleSim.Result;
using SubatomicCanvas.Model;
using SubatomicCanvas.View;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SubatomicCanvas.Presenter
{
    public class VfxVisualizePresenter : IStartable
    {
        [Inject] private LastSimulationCondition _lastSimulationCondition;
        [Inject] private VfxVisualizeView _vfxVisualizeView;
        [Inject] private TimeState _timeState;

        public void Start()
        {
            _lastSimulationCondition.result.Subscribe(OnCompletedSimulation);
            _timeState.time.Subscribe(_vfxVisualizeView.OnPassesTime);
        }
        
        private void OnCompletedSimulation(SimulationResult result)
        {
            if (result == null) return;

            _vfxVisualizeView.PlayVfx(result.LinearTrajectory);
        }
    }
}